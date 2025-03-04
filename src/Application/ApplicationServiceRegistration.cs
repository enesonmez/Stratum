using System.Reflection;
using Application.Services.UserService.Contracts;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Abstraction;
using Core.CrossCuttingConcerns.Logging.Configurations;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Localization.DI;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(mediatRServiceConfiguration =>
        {
            mediatRServiceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            mediatRServiceConfiguration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            mediatRServiceConfiguration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });
        
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        var fileLogConfiguration = GetConfiguration(configuration);
        services.AddSingleton<ILogger, SerilogFileLogger>(_ => new SerilogFileLogger(fileLogConfiguration));
        
        services.AddScoped<IUserService, UserManager>();
        
        // Localization
        services.AddFileLocalization(Assembly.GetExecutingAssembly());
        // services.AddDbLocalization();
        
        return services;
    }

    private static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }

    private static FileLogConfiguration GetConfiguration(IConfiguration configuration)
    {
        var fileLogConfiguration = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                                       .Get<FileLogConfiguration>()
                                   ?? throw new InvalidOperationException(
                                       "FileLogConfiguration section cannot found in configuration.");

        return fileLogConfiguration;
    }
}