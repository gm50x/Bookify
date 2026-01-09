using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Users.LogInUser;

internal sealed class LogInUserCommandValidator : AbstractValidator<LogInUserCommand>
{

    public LogInUserCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress();

        RuleFor(c => c.Password).NotEmpty().MinimumLength(6);
    }
}
