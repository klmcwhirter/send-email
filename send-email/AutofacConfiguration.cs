using System;
using System.IO;
using System.Net.Mail;
using Autofac;
using NLog;

namespace send_email
{
    internal static class AutofacConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(Console.In).As<TextReader>();
            builder.RegisterInstance(LogManager.GetLogger("Logger")).As<ILogger>();
            builder.RegisterType<SmtpClient>();

            builder.RegisterType<BodyBuilder>();
            builder.RegisterType<MessageBuilder>();
            builder.RegisterType<MessageSender>();
            // builder.RegisterType<BodyBuilder>();
            builder.RegisterType<Program>();

            var container = builder.Build();
            return container;
        }
    }
}