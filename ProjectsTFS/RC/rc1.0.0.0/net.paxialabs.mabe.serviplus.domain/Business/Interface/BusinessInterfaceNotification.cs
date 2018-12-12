using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Interface
{
    internal class BusinessInterfaceNotification
    {
        private string EmailDisplay;
        private string EmailHost;
        private string EmailUser;
        private string EmailPassword;
        private string EmailPort;
        private string EmailSender;
        private string EmailDestiny;


        public BusinessInterfaceNotification()
        {
            string msg = "Configurando notificación " + Environment.NewLine;
            if (ConfigurationManager.AppSettings["EmailDisplay"] == null)
            {
                msg += "No se encontraron los parámetros de configuración." + Environment.NewLine;
            }
            else
            {
                EmailDisplay = ConfigurationManager.AppSettings["EmailDisplay"];
                EmailHost = ConfigurationManager.AppSettings["EmailHost"];
                EmailUser = ConfigurationManager.AppSettings["EmailUser"];
                EmailPassword = ConfigurationManager.AppSettings["EmailPassword"];
                EmailPort = ConfigurationManager.AppSettings["EmailPort"];
                EmailSender = ConfigurationManager.AppSettings["EmailSender"];
                EmailDestiny = ConfigurationManager.AppSettings["EmailDestiny"];

                msg += "Notificación OK configurada." + Environment.NewLine;
            }
            
        }

        public void SendNotification(string messageBody)
        {
            string msg = "Enviando notificación ..." + Environment.NewLine;
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential =
                    new NetworkCredential(EmailUser, EmailPassword);
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(EmailSender);

                smtpClient.Host = EmailHost;
                smtpClient.Port = Convert.ToInt32(EmailPort);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;

                message.From = fromAddress;
                message.Subject = "Serviplus - Interface de importación ODS";
                //Set IsBodyHtml to true means you can send HTML email.
                message.IsBodyHtml = true;
                message.Body = messageBody;
                if (EmailDestiny.Contains('|'))
                {
                    foreach (var item in EmailDestiny.Split('|'))
                    {
                        message.To.Add(item);
                    }
                }
                else
                {
                    message.To.Add(EmailDestiny);
                }

                smtpClient.Send(message);

                msg += "Notificación enviada con éxito." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                msg += "Error Enviando notificación: " + ex.Message + Environment.NewLine;
                msg += "Stack: " + ex.StackTrace + Environment.NewLine;
            }
            finally
            {
                Console.WriteLine(msg);
            }
        }
    }
}
