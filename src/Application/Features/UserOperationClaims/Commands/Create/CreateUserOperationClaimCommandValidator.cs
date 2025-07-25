using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommandRequest>
{
    public CreateUserOperationClaimCommandValidator()
    {
        RuleFor(c => c.UserId).NotNull();
        RuleFor(c => c.OperationClaimId).GreaterThan(0);
    }
}