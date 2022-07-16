using MailKit.Net.Smtp;
using MimeKit;

namespace RThirst.Tools
{
    public static class EMail
    {
        public static void SendEmail()
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("RB", "rokbot@inbox.ru"));
            mailMessage.To.Add(new MailboxAddress("zxc", "richardfack3@gmail.com"));
            mailMessage.Subject = "VERIFICATION";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "ROK requires verification"
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.mail.ru", 465, true);
                smtpClient.Authenticate("rokbot@inbox.ru", "ZpWyBRLjF6SL8eNdXz2y");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
