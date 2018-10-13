using System.Text;
using MimeKit;

namespace send_email
{
    public class MessageBuilder
    {
        public MimeMessage Build(SendEmailClo opts, string body)
        {
            var rc = new MimeMessage
            {
                Body = new TextPart("html") { Text = body },
                Subject = opts.Subject,
            };

            opts.From.SetInternetAddressList(rc.From);
            rc.Sender = new MailboxAddress(rc.From[0]?.ToString());

            opts.To.SetInternetAddressList(rc.To);

            return rc;
        }
    }
}