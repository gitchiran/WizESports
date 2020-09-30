using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace Wiz_eSports_Management.Common
{
    public static class Email
    {
        public static bool SendEmail(string To, string From, string Subject, string Body, string Password, string Host, int PortNo,string FilePath="")
        {
            try
            {
                var builder = new BodyBuilder();
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(From));
                mimeMessage.To.Add(new MailboxAddress(To));
                mimeMessage.Subject = Subject;

                builder.HtmlBody = Body;
                ////////mimeMessage.Body = new TextPart("html")
                ////////{
                ////////    Text = Body

                ////////};

                if (FilePath!="")
                {
                    builder.Attachments.Add(@FilePath);
                }

                ////mimeMessage.Body = new TextPart("html")
                ////{
                ////    Text = Body

                ////};

                mimeMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(Host, PortNo, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(From, Password);
                    client.Send(mimeMessage);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
