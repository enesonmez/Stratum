using Application.Repositories.OperationClaims;
using Application.Repositories.RefreshTokens;
using Application.Repositories.UserOperationClaims;
using Application.Repositories.Users;
using Core.Localization.DB.Repositories.Resources;
using Core.Persistence.DI;
using Core.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.OperationClaims;
using Persistence.Repositories.RefreshTokens;
using Persistence.Repositories.UserOperationClaims;
using Persistence.Repositories.Users;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("BaseDb"));
        services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

        services.AddScoped<IUnitOfWork, EfUnitOfWork<BaseDbContext>>();
        
        services.AddScoped<IResourceReadRepository, ResourceReadRepository<BaseDbContext>>();
        
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        services.AddScoped<IOperationClaimReadRepository, OperationClaimReadRepository>();
        services.AddScoped<IOperationClaimWriteRepository, OperationClaimWriteRepository>();
        
        services.AddScoped<IUserOperationClaimReadRepository, UserOperationClaimReadRepository>();
        services.AddScoped<IUserOperationClaimWriteRepository, UserOperationClaimWriteRepository>();
        
        services.AddScoped<IRefreshTokenReadRepository, RefreshTokenReadRepository>();
        services.AddScoped<IRefreshTokenWriteRepository, RefreshTokenWriteRepository>();
        
        
        return services;
    }
}