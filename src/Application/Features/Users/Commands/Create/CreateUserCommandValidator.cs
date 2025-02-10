using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}