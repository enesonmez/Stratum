using System.Reflection;
using Core.Localization.DB.Entities;
using Core.Localization.DB.EntityConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    // Localization
    public DbSet<Resource> Resources { get; set; }
    public DbSet<ResourceTranslation> ResourceTranslations { get; set; }
    // Authentication
    public DbSet<User> Users { get; set; }      
    public DbSet<OperationClaim> OperationClaims { get; set; }      

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceTranslationConfiguration());
    }
}