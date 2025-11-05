using Bookify.Domain.Users;

namespace Bookify.Application.Abstractions.Mailing;
public interface IEmailService
{
    Task SendAsync(Email recipient, string subject, string body);
}
