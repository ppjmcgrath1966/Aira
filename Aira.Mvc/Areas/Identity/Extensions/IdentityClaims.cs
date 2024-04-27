namespace Aira.Mvc.Areas.Identity.Extensions;

public static class IdentityClaims
{
    public static string GetCurrentUserProperty(this ClaimsPrincipal user, string claimType)
    {
        if (user.Identity is { IsAuthenticated: true })
        {
            return user.Claims.FirstOrDefault(v => v.Type == claimType)?.Value ?? string.Empty;
        }
        return string.Empty;
    }
}