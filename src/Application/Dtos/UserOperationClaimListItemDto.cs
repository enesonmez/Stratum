using Core.Application.Dtos;

namespace Application.Dtos;

public class UserOperationClaimListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public int OperationClaimId { get; set; }
    public string OperationClaimName { get; set; } = string.Empty;
}