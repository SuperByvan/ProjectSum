using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(assembly);
        services.AddSingleton(sp =>
        {
            return Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromMinutes(1)
                );
        });
        return services;
    }
}