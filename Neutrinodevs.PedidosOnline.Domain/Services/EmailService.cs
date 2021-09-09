using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Domain.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }

        public void Send(EmailParams emailParams)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(emailParams.SenderEmail, emailParams.SenderName);
                msg.Subject = emailParams.Subject;
                msg.To.Add(emailParams.EmailTo);
                msg.BodyEncoding = Encoding.UTF8;
                msg.Body = emailParams.Body;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("delivery.lapesca@gmail.com", "Lapesca.bby123");

                string output = null;

                try
                {
                    smtp.Send(msg);
                    msg.Dispose();
                    output = "Correo electrónico fue enviado satisfactoriamente.";
                }
                catch (Exception ex)
                {
                    output = "Error enviando correo electrónico: " + ex.Message;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
