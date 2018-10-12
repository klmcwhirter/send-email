using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace send_email.Tests
{
    [TestClass]
    public class MessageBuilderTests
    {
        [TestMethod]
        public void CanConstruct()
        {
            new MessageBuilder();
        }

        [TestMethod]
        public void BuildsFromOpts()
        {
            var opts = new SendEmailClo()
            {
                Filename = null,
                From = "from@x.a",
                To = "to@x.a",
                Subject = "subject"
            };
            var builder = new MessageBuilder();

            var rc = builder.Build(opts, "body");

            Assert.AreEqual("body", rc.Body, false, "Should set Body");
            Assert.AreEqual(Encoding.UTF8, rc.BodyEncoding, "Should set BodyEncoding");

            Assert.AreEqual(opts.From, rc.From.Address, false, "Should have set From");
            Assert.AreEqual(opts.To, rc.To[0].Address, false, "Should have set To");

            Assert.AreEqual(opts.Subject, rc.Subject, false, "Should set Subject");
            Assert.AreEqual(Encoding.UTF8, rc.SubjectEncoding, "Should set SubjectEncoding");
        }
    }
}
