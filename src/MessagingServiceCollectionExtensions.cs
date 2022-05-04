using Microsoft.Extensions.DependencyInjection;
using Ximo.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Ximo.EmailMessaging.SendGrid;

public static class MessagingServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the messaging module to the <see cref="ServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The service collection to which the module is to be added.</param>
    /// <returns>The service collection with the added messaging module.</returns>
    public static IServiceCollection AddMessagingModule(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddModule<MessagingModule>(configuration);
        return serviceCollection;
    }
}