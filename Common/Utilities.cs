using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Utilities
    {
        public static void SendEmails(string emailFrom, string emailIdList, string subject, string bodyText)
        {
            
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailIdList);

                mail.Subject = subject;
                mail.Body = bodyText;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Headers.Add("Content-Type", "text/html");
                
                SmtpClient client = new SmtpClient();
                client.Send(mail);

            }


        }
        
        
        public static void SendEmails(string emailFrom, string emailIdList, string subject, string bodyText, AlternateView avHTML)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.AlternateViews.Add(avHTML);
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailIdList);

                mail.Subject = subject;
                mail.Body = bodyText;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Send(mail);

            }


        }
    }
}
