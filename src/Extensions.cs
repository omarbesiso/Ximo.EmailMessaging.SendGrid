using Ximo.Validation;

namespace Ximo.EmailMessaging.SendGrid;

internal static class Extensions
{
    public static global::SendGrid.Helpers.Mail.EmailAddress ToSendGridEmailAddress(this EmailAddress emailAddress)
    {
        Check.NotNull(emailAddress, nameof(emailAddress));
        Check.NotNullOrWhitespace(emailAddress.Address, nameof(emailAddress.Address));

        return new global::SendGrid.Helpers.Mail.EmailAddress(emailAddress.Address, emailAddress.DisplayName);
    }
}