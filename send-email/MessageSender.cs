using System;
using System.Net.Mail;
using System.Threading.Tasks;
using NLog;

namespace send_email
{
    public class MessageSender
    {
        protected Func<string, int, SmtpClient> SmtpClientFunc { get; }
        protected ILogger Logger { get; set; }

        public MessageSender(
            Func<string, int, SmtpClient> smtpClientFunc,
            ILogger logger
            )
        {
            SmtpClientFunc = smtpClientFunc;
            this.Logger = logger;
        }

        public async Task<bool> Send(SendEmailClo opts, MailMessage message, string token = null)
        {
            var rc = false;

            using (var client = SmtpClientFunc(opts.Host, int.Parse(opts.Port)))
            {
                token = token ?? Guid.NewGuid().ToString();

                client.SendCompleted += (sender, e) =>
                {
                    if (e.Cancelled)
                    {
                        Logger.Info($"[{token}] Send canceled.");
                    }
                    if (e.Error != null)
                    {
                        Logger.Error(e.Error, $"[{token}]");
                    }
                    else
                    {
                        rc = true;
                        Logger.Info($"[{token}] Message sent.");
                    };
                };

                await client.SendMailAsync(message);

                return rc;
            }
        }
    }
}
