using System.Linq;
using MimeKit;

namespace send_email
{
    public static class StringExtensions
    {
        public static void SetInternetAddressList(this string addrs, InternetAddressList target)
        {
            addrs.Split(new[] { ',', ';' }).ToList().ForEach(a => target.Add(new MailboxAddress(a)));
        }
    }
}