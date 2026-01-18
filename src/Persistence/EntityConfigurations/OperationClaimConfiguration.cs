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

        builder.HasMany(oc => oc.UserOperationClaims).WithOne(uoc => uoc.OperationClaim)
            .HasForeignKey(uoc => uoc.OperationClaimId);

        builder.HasData(Seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;

    private IEnumerable<object> Seeds
    {
        get
        {
            yield return new { Id = AdminId, Name = GeneralOperationClaims.Admin, CreatedDate = DateTime.UtcNow };

            IEnumerable<object> featureOperationClaims = GetFeatureOperationClaims(AdminId);
            foreach (var claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<object> GetFeatureOperationClaims(int initialId)
    {
        var lastId = initialId;
        List<object> featureOperationClaims = [];

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
                new { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = OperationClaimsOperationClaims.Read, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = OperationClaimsOperationClaims.Write, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = OperationClaimsOperationClaims.Create, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = OperationClaimsOperationClaims.Update, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete, CreatedDate = DateTime.UtcNow },
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
                new { Id = ++lastId, Name = UsersOperationClaims.Admin, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = UsersOperationClaims.Read, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = UsersOperationClaims.Write, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = UsersOperationClaims.Create, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = UsersOperationClaims.Update, CreatedDate = DateTime.UtcNow },
                new { Id = ++lastId, Name = UsersOperationClaims.Delete, CreatedDate = DateTime.UtcNow },
            ]
        );

        #endregion

        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}