using System.Reflection;
using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;
using Sample1.Application.Common.Behaviours;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, Configuration.IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        var serviceBusConnectionString = configuration["MessageBroker:ConnectionString"];
        Guard.Against.Null(serviceBusConnectionString, message: "Azure Service Bus connection string not found.");

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.UsingAzureServiceBus((context, configurator) =>
            {
                configurator.Host(serviceBusConnectionString);
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
