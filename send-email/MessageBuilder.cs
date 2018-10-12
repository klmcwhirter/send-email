using System.Net.Mail;
using System.Text;

namespace send_email
{
    public class MessageBuilder
    {
        public MailMessage Build(SendEmailClo opts, string body)
        {
            var rc = new MailMessage(opts.From, opts.To)
            {
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Subject = opts.Subject,
                SubjectEncoding = Encoding.UTF8
            };
            return rc;
        }
    }
}