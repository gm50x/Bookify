namespace Bookify.WebApi.Controllers.Users;

public sealed record LogInUserRequest(
    string Email,
    string Password);
