﻿using Bookify.Domain.Users;

namespace Bookify.Application.Abstractions.Mailing;
internal interface IEmailService
{
    Task SendAsync(Email recipient, string subject, string body);
}
