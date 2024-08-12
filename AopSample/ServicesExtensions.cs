using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AopSample;

public static class ServicesExtensions
{
    public static void AddProxiedScoped<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.TryAddSingleton<IProxyGenerator, ProxyGenerator>();
        services.TryAddScoped<TImplementation>();

        services.AddScoped(typeof(TInterface), serviceProvider =>
        {
            var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
            var actual = serviceProvider.GetRequiredService<TImplementation>();
            var interceptors = serviceProvider.GetServices<IInterceptor>().ToArray();
            return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface),
                actual,
                ProxyGenerationOptions.Default,
                interceptors);
        });
    }

    public static void AddProxiedScoped<TImplementation>(this IServiceCollection services)
        where TImplementation : class
    {
        services.TryAddSingleton<IProxyGenerator, ProxyGenerator>();
        services.AddScoped<TImplementation>();
        services.AddScoped(serviceProvider =>
        {
            var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
            var actual = serviceProvider.GetRequiredService<TImplementation>();
            var interceptors = serviceProvider.GetServices<IInterceptor>().ToArray();
            return proxyGenerator.CreateClassProxyWithTarget(
                typeof(TImplementation),
                actual,
                ProxyGenerationOptions.Default,
                interceptors);
        });
    }
}