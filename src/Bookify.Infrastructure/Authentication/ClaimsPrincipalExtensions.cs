using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bookify.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    private const string IdentityUnavailable = "User identity is unavailable";
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var rawUserId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? throw new ApplicationException(IdentityUnavailable);

        return Guid.TryParse(rawUserId, out var parsedUserId)
            ? parsedUserId
            : throw new ApplicationException(IdentityUnavailable);
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
        => principal?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ApplicationException(IdentityUnavailable);
}
