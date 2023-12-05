using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NutriApp.Controllers.Middleware;

public class NutriAppAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly App _app;
    
    public NutriAppAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        App app)
        : base(options, logger, encoder, clock)
    {
        _app = app;
    }

    public const string SessionHeaderName = "sessionKey";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Get the session header value
        if (!Request.Headers.TryGetValue(SessionHeaderName, out var sessionHeaderValues))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing session header"));
        }
        
        // Get the session key
        var sessionKey = sessionHeaderValues[0]!;
        
        var user = _app.UserControl.GetUser(Guid.Parse(sessionKey));
        if (user == null)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid session key"));
        }
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName), 
            new Claim("SessionKey", sessionKey)
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);

        return Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(
                principal,
                new AuthenticationProperties(),
                Scheme.Name)));
    }
}