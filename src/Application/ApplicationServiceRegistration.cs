using System.Reflection;
using Application.Features.Auth.Mappers;
using Application.Features.OperationClaims.Mappers;
using Application.Features.UserOperationClaims.Mappers;
using Application.Features.Users.Mappers;
using Application.Services.AuthService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Performance;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Logging.Abstraction;
using Core.CrossCuttingConcerns.Logging.Configurations;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Security.DI;
using Core.Security.Abstractions.Jwt;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(mediatRServiceConfiguration =>
        {
            mediatRServiceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            mediatRServiceConfiguration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            mediatRServiceConfiguration.AddOpenBehavior(typeof(PerformanceBehavior<,>));
            mediatRServiceConfiguration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            mediatRServiceConfiguration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            mediatRServiceConfiguration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });
        
        // Mapperly
        services.AddSingleton<UserMapper>();
        services.AddSingleton<OperationClaimMapper>();
        services.AddSingleton<UserOperationClaimMapper>();
        services.AddSingleton<AuthMapper>();
        
        // Logging
        var fileLogConfiguration = GetFileLogConfiguration(configuration);
        services.AddSingleton<ISeriLogSinkProvider>(new SerilogFileLogSinkProvider(fileLogConfiguration));
        services.AddSingleton<ILogger, SeriLogLogger>();
        
        // Services
        services.AddScoped<IAuthService, AuthManager>();
        
        // Security
        var tokenOptions =  GetTokenOptions(configuration);
        services.AddSecurityServices<Guid, int, Guid>(tokenOptions);
        
        return services;
    }


    private static FileLogConfiguration GetFileLogConfiguration(IConfiguration configuration)
    {
        var fileLogConfiguration = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                                       .Get<FileLogConfiguration>()
                                   ?? throw new InvalidOperationException(
                                       "FileLogConfiguration section cannot found in configuration.");

        return fileLogConfiguration;
    }
    
    private static TokenOptions GetTokenOptions(IConfiguration configuration)
    {
        const string tokenOptionsConfigurationSection = "TokenOptions";
        TokenOptions tokenOptions =
            configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new InvalidOperationException(
                $"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration.");

        return tokenOptions;
    }
}