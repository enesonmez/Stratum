using Core.Security.Constants;
using Domain.Constants.Claims;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);
        
        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");
        
        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(Seeds);
        
        builder.HasBaseType((string)null!);
        
    }

    private static int AdminId => 1;
    private IEnumerable<OperationClaim> Seeds
    {
        get
        {
            yield return new OperationClaim { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = GetFeatureOperationClaims(AdminId);
            foreach (var claim in featureOperationClaims)
                yield return claim;
        }
    }
    
    #pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> GetFeatureOperationClaims(int initialId)
    {
        var lastId = initialId;
        List<OperationClaim> featureOperationClaims = [];

        // #region Auth
        // featureOperationClaims.AddRange(
        //     [
        //         new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
        //         new() { Id = ++lastId, Name = AuthOperationClaims.Read },
        //         new() { Id = ++lastId, Name = AuthOperationClaims.Write },
        //         new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
        //     ]
        // );
        // #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new OperationClaim { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new OperationClaim { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new OperationClaim { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new OperationClaim { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new OperationClaim { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new OperationClaim { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        // #region UserOperationClaims
        // featureOperationClaims.AddRange(
        //     [
        //         new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
        //         new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
        //         new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
        //         new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
        //         new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
        //         new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
        //     ]
        // );
        // #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new OperationClaim { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new OperationClaim { Id = ++lastId, Name = UsersOperationClaims.Read },
                new OperationClaim { Id = ++lastId, Name = UsersOperationClaims.Write },
                new OperationClaim { Id = ++lastId, Name = UsersOperationClaims.Create },
                new OperationClaim { Id = ++lastId, Name = UsersOperationClaims.Update },
                new OperationClaim { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        return featureOperationClaims;
    }
    #pragma warning restore S1854 // Unused assignments should be removed
}