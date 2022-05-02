using Newtonsoft.Json;

namespace Ximo.EmailMessaging.SendGrid;

[JsonObject(SectionName)]
public class SendGridEmailSenderOptions
{
    public const string SectionName = "SendGrid";

    /// <summary>
    ///     The SendGrid Api Key.
    /// </summary>
    public string ApiKey { get; set; } = null!;

    /// <summary>
    ///     Gets the default sender email when no sender email is provided.
    /// </summary>
    public string DefaultSenderEmail { get; set; } = null!;

    /// <summary>
    ///     Gets the default sender name when no sender name is provided.
    /// </summary>
    public string DefaultSenderName { get; set; } = null!;
}