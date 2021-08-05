using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    public class EmailHandling
    {

        public void SendEmail(string emailTo, string subject, string body)
        {
            try
            {

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(MailSettings.Default.MailFrom),
                    Subject = subject,
                    Body = body
                };

                mail.To.Add(emailTo);

                SmtpClient smtpClient = new SmtpClient(MailSettings.Default.SmtpServer)
                {
                    Port = MailSettings.Default.Port,
                    Credentials = new NetworkCredential(MailSettings.Default.MailFrom, MailSettings.Default.Password),
                    EnableSsl = MailSettings.Default.EnableSsl
                };

                smtpClient.Send(mail);

            }
            catch (Exception)
            {}
        }

    }
}
