using System;

namespace NutriApp;

public record LoginResult(User User, Guid Guid, LoginResultType Type);

public enum LoginResultType
{
    Valid,
    InvalidPassword,
    InvalidUsername
}