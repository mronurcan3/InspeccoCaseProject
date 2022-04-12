using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    /// <summary>
    /// Handle send/receive email requirements
    /// </summary>
    public static class MailService
    {
        /// <summary>
        /// Send email using smtp client 
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="password"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="sender"></param>
        public static void Send(string receiver, string password = "MRnobody1905$$.US", string body = "Test Message", string subject = "Test Email", string sender = "anketler3@gmail.com")
        {
            MailAddress senderEmail = new MailAddress(sender);
            MailAddress receiverEmail = new MailAddress(receiver);

            using (SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)

            })

            using (MailMessage message = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body,

            })
            {
                smtp.Send(message);
            }

        }
    }
}
