using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ximo.DependencyInjection;

namespace Ximo.EmailMessaging.SendGrid;

public class MessagingModule : IModule
{
    public void Initialize(IServiceCollection serviceCollection)
    {
        var sendGridSection = Configuration?.GetSection(SendGridEmailSenderOptions.SectionName);
        serviceCollection.Configure<SendGridEmailSenderOptions>(sendGridSection);

        serviceCollection.AddSingleton<IEmailService, SendGridEmailService>();
    }

    public IConfiguration? Configuration { get; set; }
}