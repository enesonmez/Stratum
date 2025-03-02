using FluentValidation;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommandRequest>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}