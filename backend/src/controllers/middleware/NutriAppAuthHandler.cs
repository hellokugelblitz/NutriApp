using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    public const string SESSION_HEADER_NAME = "sessionKey";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Check that the endpoint that is being called
        // requires authorization.
        // It should be noted that normally this would be done automatically by the framework,
        // but for some reason it isn't
        if (Context.GetEndpoint()?.Metadata.GetMetadata<IAuthorizeData>() == null)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        
        // Get the session header value
        if (!Request.Headers.TryGetValue(SESSION_HEADER_NAME, out var sessionHeaderValues))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing session header"));
        }
        
        // Get the session key
        var sessionKey = sessionHeaderValues[0]!;

        Guid.TryParse(sessionKey, out var sessionGuid);
        var user = _app.UserControl.GetUser(sessionGuid);
        if (user == null)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid session key"));
        }

        Context.Items["User"] = user;
        
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