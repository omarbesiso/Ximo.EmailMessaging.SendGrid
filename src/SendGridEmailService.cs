using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Ximo.Utilities.Extensions;
using Ximo.Validation;

namespace Ximo.EmailMessaging.SendGrid;

/// <summary>
///     Contract for Email sending.
/// </summary>
internal class SendGridEmailService : IEmailService
{
    public SendGridEmailService(IOptions<SendGridEmailSenderOptions> optionsAccessor)
    {
        Options = optionsAccessor.Value;
    }

    public SendGridEmailSenderOptions Options { get; }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var sender = new EmailAddress(Options.DefaultSenderEmail, Options.DefaultSenderName);
        var recipient = new EmailAddress(email);

        await SendEmailAsync(subject, sender, new List<EmailAddress> {recipient}, htmlMessage);
    }

    public async Task<bool> SendEmailAsync(string subject, EmailAddress senderEmail,
        IEnumerable<EmailAddress> toRecipients, string? htmlMessage = null, string? txtMessage = null,
        IEnumerable<EmailAddress>? ccRecipients = null,
        IEnumerable<EmailAddress>? bccRecipients = null)
    {
        Check.NotNull(senderEmail, nameof(senderEmail));
        Check.NotNullOrWhitespace(senderEmail.Address, nameof(senderEmail));
        Check.NotNullOrEmpty(toRecipients, nameof(toRecipients));

        var msg = new SendGridMessage
        {
            From = senderEmail.ToSendGridEmailAddress(),
            Subject = subject,
            PlainTextContent = txtMessage,
            HtmlContent = htmlMessage
        };

        foreach (var toEmailAddress in toRecipients)
        {
            msg.AddTo(toEmailAddress.ToSendGridEmailAddress());
        }

        // ReSharper disable once PossibleMultipleEnumeration
        if (ccRecipients.IsNotNullOrEmpty())
        {
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var ccEmailAddress in ccRecipients!)
            {
                msg.AddCc(ccEmailAddress.ToSendGridEmailAddress());
            }
        }

        // ReSharper disable once PossibleMultipleEnumeration
        if (bccRecipients.IsNotNullOrEmpty())
        {
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var bccEmailAddress in bccRecipients!)
            {
                msg.AddBcc(bccEmailAddress.ToSendGridEmailAddress());
            }
        }

        msg.SetClickTracking(false, false);
        msg.SetOpenTracking(false);
        msg.SetGoogleAnalytics(false);
        msg.SetSubscriptionTracking(false);

        var client = new SendGridClient(Options.ApiKey);
        var response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode;
    }
}