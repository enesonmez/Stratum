using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");
        
        builder.Property(u=>u.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);
        
        builder.HasMany(u=>u.UserOperationClaims).WithOne(uoc=>uoc.User).HasForeignKey(uoc=>uoc.UserId);
        builder.HasMany(u => u.RefreshTokens).WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId);

        builder.HasData(Seeds);
        
        builder.HasBaseType((string)null!);
    }

    public static Guid AdminId { get; } = Guid.NewGuid();
    private IEnumerable<object> Seeds
    {
        get
        {
            HashingHelper.CreatePasswordHash(
                password: "123456",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            var adminUser =
                new
                {
                    Id = AdminId,
                    Email = "stratum@gmail.com",
                    FirstName = "Stratum",
                    LastName = "Stratum",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    AuthenticatorType = Core.Security.Enums.AuthenticatorType.None,
                    CreatedDate = DateTime.UtcNow
                };
            yield return adminUser;
        }
    }
}