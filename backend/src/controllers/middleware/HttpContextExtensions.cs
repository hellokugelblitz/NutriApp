using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NutriApp.Controllers.Middleware;

public static class HttpContextExtensions
{
    /// <summary>
    /// Gets the current user from the HttpContext.
    /// *Should only be called from within a controller action that requires authentication.
    /// If not, then user may not be guaranteed*
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <returns>The current user</returns>
    public static User GetUser(this HttpContext context)
    {
        return (User) context.Items["User"]!;
    }
    
    public static Guid GetSessionKey(this HttpContext context)
    {
        var sessionKey = context.User.Claims
            .FirstOrDefault(c => c.Type == NutriAppAuthHandler.SESSION_CLAIM_NAME)!.Value;
        Guid.TryParse(sessionKey, out var guid);
        return guid;
    }
}