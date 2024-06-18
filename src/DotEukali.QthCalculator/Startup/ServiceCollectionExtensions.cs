using DotEukali.QthCalculator.Wrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotEukali.QthCalculator.Startup
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Tries to add the MaidenheadCalculator service to the IServiceCollection.
        /// </summary>
        /// <param name="serviceCollection">The IServiceCollection to add the service to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection TryAddMaidenheadCalculator(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IMaidenheadCalculator, MaidenheadCalculatorWrapper>();

            return serviceCollection;
        }
    }
}
