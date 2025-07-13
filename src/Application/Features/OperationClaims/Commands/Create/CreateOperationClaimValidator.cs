using FluentValidation;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimValidator : AbstractValidator<CreateOperationClaimCommandRequest>
{
    public CreateOperationClaimValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
    }
}