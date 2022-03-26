using Microsoft.Extensions.DependencyInjection;

namespace PassThroughScopeSample
{
    public static class ServiceProviderExtension
    {
        public static IServiceScope CreateServiceScope(this IServiceProvider provider)
        {
            var newScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            // Copy tenant identifier values from original scope to the new scope
            var originalTenantIdentifier = provider.GetRequiredService<TenantIdentifier>();
            var newTenantIdentifier = newScope.ServiceProvider.GetRequiredService<TenantIdentifier>();
            newTenantIdentifier.TenantId = originalTenantIdentifier.TenantId;

            return newScope;
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This is only to interfere and break the build if someone tries to use the standard .NET CreateScope() extension function.
    /// We need to enforce usage of CreateServiceScope() custom extension function to make sure the necessary pass through variables are managed.
    /// </summary>
    public static class ServiceProviderServiceExtensions
    {
        [Obsolete("This will throw error use CreateServiceScope instead.")]
        public static IServiceScope CreateScope(this IServiceProvider provider)
        {
            throw new NotSupportedException("Do not use CreateScope() ");
        }
    }
}
