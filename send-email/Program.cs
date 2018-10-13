using System.IO;
using Autofac;
using CommandLine;
using NLog;

namespace send_email
{
    public class SendEmailClo
    {
        [Option('f', "filename", HelpText = "Optional filename to use as body. default: stdin")]
        public string Filename { get; set; }

        [Option('F', "from", HelpText = "The sender address of the email", Required = true)]
        public string From { get; set; }

        [Option('T', "to", HelpText = "Recipient address(es)", Required = true)]
        public string To { get; set; }

        [Option('S', "subject", HelpText = "The email subject line", Required = true)]
        public string Subject { get; set; }

        [Option('h', "host", HelpText = "The SMTP server host", Required = true)]
        public string Host { get; set; }

        [Option('p', "port", HelpText = "The SMTP server port number", Required = true)]
        public string Port { get; set; }
    }

    public class Program
    {
        protected BodyBuilder BodyBuilder { get; }
        protected MessageBuilder MessageBuilder { get; }
        protected MessageSender MessageSender { get; }
        protected TextReader TextReader { get; }
        protected ILogger Logger { get; set; }

        public Program(
            BodyBuilder bodyBuilder,
            MessageBuilder messageBuilder,
            MessageSender messageSender,
            TextReader textReader,
            ILogger logger
        )
        {
            BodyBuilder = bodyBuilder;
            MessageBuilder = messageBuilder;
            MessageSender = messageSender;
            TextReader = textReader;
            this.Logger = logger;
        }

        public int Run(string[] args)
        {
            var rc = Parser.Default.ParseArguments<SendEmailClo>(args)
                .MapResult(
                    (SendEmailClo opts) =>
                    {
                        var body = BodyBuilder.Build(opts, TextReader);
                        var message = MessageBuilder.Build(opts, body);
                        var success = MessageSender.Send(opts, message);
                        return success ? 0 : 255;
                    },
                    (errors) =>
                    {
                        Logger.Error(errors);
                        return 255;
                    }
                );
            return rc;
        }

        static int Main(string[] args)
        {
            var container = AutofacConfiguration.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<Program>();
                var rc = app.Run(args);
                return rc;
            }
        }
    }
}
