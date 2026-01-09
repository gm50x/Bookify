namespace Bookify.Infrastructure.Authentication;

public sealed class KeycloakOptions
{
    public string AdminUrl { get; init; } = String.Empty;
    public string TokenUrl { get; init; } = String.Empty;
    public string AdminClientId { get; init; } = String.Empty;
    public string AdminClientSecret { get; init; } = String.Empty;
    public string AuthClientId { get; init; } = String.Empty;
    public string AuthClientSecret { get; init; } = String.Empty;

}

