using System;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using NLog;

namespace send_email
{
    public class MessageSender
    {
        protected Func<SmtpClient> SmtpClientFunc { get; }
        protected ILogger Logger { get; set; }

        public MessageSender(
            Func<SmtpClient> smtpClientFunc,
            ILogger logger
            )
        {
            SmtpClientFunc = smtpClientFunc;
            this.Logger = logger;
        }

        public bool Send(SendEmailClo opts, MimeMessage message, string token = null)
        {
            using (var client = SmtpClientFunc())
            {
                token = token ?? Guid.NewGuid().ToString();

                var rc = false;
                try
                {
                    client.Connect(opts.Host, int.Parse(opts.Port));
                    // client.Authenticate("user", "password");
                    client.Send(message);
                    client.Disconnect(true);

                    rc = true;
                    Logger.Info($"[{token}] Message sent.");
                }
                catch (Exception e)
                {
                    Logger.Error(e, $"[{token}]");
                }

                return rc;
            }
        }
    }
}
