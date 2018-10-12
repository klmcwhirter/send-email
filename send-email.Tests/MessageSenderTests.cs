using System;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;

namespace send_email.Tests
{
    [TestClass]
    public class MessageSenderTests
    {
        [TestMethod]
        public void CanConstruct()
        {
            new MessageSender(null, null);
        }

        SendEmailClo opts = new SendEmailClo()
        {
            Filename = null,
            From = "from@x.a",
            To = "to@x.a",
            Subject = "subject",

            Host = "host",
            Port = "12345"
        };

        public int Port => int.Parse(opts.Port);

        Mock<ILogger> LoggerMock { get; set; }
        Mock<SmtpClient> SmtpClientMock { get; set; }
        Func<string, int, SmtpClient> Factory { get; set; }

        [TestInitialize]
        public void Setup()
        {
            LoggerMock = new Mock<ILogger>();
            SmtpClientMock = new Mock<SmtpClient>();
            Factory = (h, p) =>
            {
                SmtpClientMock.Object.Host = h;
                SmtpClientMock.Object.Port = p;
                return SmtpClientMock.Object;
            };
        }

        [TestMethod]
        public void SendPassesCorrectHostPort()
        {
            var sender = new MessageSender(Factory, LoggerMock.Object);

            var rc = sender.Send(opts, new MailMessage());

            Assert.AreEqual(opts.Host, SmtpClientMock.Object.Host, false, "Should set Host");
            Assert.AreEqual(Port, SmtpClientMock.Object.Port, "Should set Port");
        }
    }
}
