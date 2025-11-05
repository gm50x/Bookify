using Bookify.Application.Abstractions.Mailing;
using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Mailing;


internal class EmailService : IEmailService
{
    public Task SendAsync(Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
