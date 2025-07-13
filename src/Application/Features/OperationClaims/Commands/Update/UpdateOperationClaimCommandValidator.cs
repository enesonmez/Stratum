using FluentValidation;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommandValidator : AbstractValidator<UpdateOperationClaimCommandRequest>
{
    public UpdateOperationClaimCommandValidator()
    {
        RuleFor(c=>c.Name).NotEmpty().MinimumLength(2);
    }
}