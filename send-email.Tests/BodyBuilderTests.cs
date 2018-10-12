using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace send_email.Tests
{
    [TestClass]
    public class BodyBuilderTests
    {
        [TestMethod]
        public void CanConstruct()
        {
            new BodyBuilder();
        }

        [TestMethod]
        public void BuildsFromAlternateReader()
        {
            var opts = new SendEmailClo();
            var alt = new StringReader("test text");
            var builder = new BodyBuilder();

            var rc = builder.Build(opts, alt);

            Assert.AreEqual("test text", rc, false, "Should receive text from alternate TextWriter");
        }
    }
}
