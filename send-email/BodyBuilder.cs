using System.IO;

namespace send_email
{
    public class BodyBuilder
    {
        public string Build(SendEmailClo opts, TextReader alternate)
        {
            var rc = string.Empty;

            if(opts.Filename != null)
            {
                rc = File.ReadAllText(opts.Filename);
            }
            else
            {
                rc = alternate.ReadToEnd();
            }

            return rc;
        }
    }
}