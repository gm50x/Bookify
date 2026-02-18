using Bookify.Application.Bookings.GetLoggedInUser;
using Bookify.Application.Users.LogInUser;
using Bookify.Application.Users.RegisterUser;
using Bookify.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.WebApi.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("me")]
    [HasPermission(Permissions.UsersRead)]
    public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetLoggedInUserQuery(), cancellationToken);

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        //return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(new { id = result.Value });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LogIn(
    [FromBody] LogInUserRequest request,
    CancellationToken cancellationToken)
    {
        var command = new LogInUserCommand(
            request.Email,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}
