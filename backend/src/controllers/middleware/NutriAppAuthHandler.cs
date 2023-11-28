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
    public NutriAppAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }


    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Session header name
        var sessionHeaderName = "sessionKey";
        
        // Get the session header value
        if (!Request.Headers.TryGetValue(sessionHeaderName, out var sessionHeaderValues))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing session header"));
        }
        
        // Create claim for dummy user
        var claims = new[] { new Claim(ClaimTypes.Name, "dummy") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        
        // Return success for now if session key exists
        return Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(
                principal,
                new AuthenticationProperties(),
                Scheme.Name)));
    }
}