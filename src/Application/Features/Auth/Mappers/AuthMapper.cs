using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Features.Auth.Mappers;

[Mapper]
public partial class AuthMapper
{
    #pragma warning disable RMG020
    #pragma warning disable RMG012
    public partial RefreshToken ToRefreshToken(Core.Security.Abstractions.Entities.RefreshToken<Guid, Guid> coreRefreshToken);
    #pragma warning restore RMG012
    #pragma warning restore RMG020
}