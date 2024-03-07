using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.User;

public class CreateUserValidator : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(15);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(15);
    }
}