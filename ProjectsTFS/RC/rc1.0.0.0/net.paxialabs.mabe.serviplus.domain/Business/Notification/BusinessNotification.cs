using Microsoft.Exchange.WebServices.Data;
using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Interface;
using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace net.paxialabs.mabe.serviplus.domain.Business.Notification
{
    internal class BusinessNotification
    {
         
        private string EmailSender { get; set; }
        private string EmailDisplay { get; set; }
        private string EmailUser { get; set; }
        private string EmailPassword { get; set; }
        private string EmailHost { get; set; }
        private string EmailDestiny { get; set; }
        private int EmailPort { get; set; }

        public BusinessNotification()
        {
            EmailDisplay = GlobalConfiguration.StringMailDisplay;
            EmailSender = GlobalConfiguration.StringMailSender;
            EmailUser = GlobalConfiguration.StringMailUser;
            EmailPassword = GlobalConfiguration.StringMailPassword;
            EmailHost = GlobalConfiguration.StringMailHost;
            EmailPort = Convert.ToInt32(GlobalConfiguration.StringMailPort);
            EmailDestiny = GlobalConfiguration.StringMailDestiny;
        }

        public void SendMailExchange(string User, string Password, List<string> MailTo, string MailTitle, string MailBody, string File = "")
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);

            service.Url = new Uri(GlobalConfiguration.exchangeURL);
            
            //service.UseDefaultCredentials = true;
            service.Credentials = new WebCredentials(User, Password);
            
            EmailMessage message = new EmailMessage(service);
           
            message.Subject = MailTitle;
            message.Body = MailBody;
            foreach (var item in MailTo)
                message.ToRecipients.Add(item);

            if(File != "") message.Attachments.AddFileAttachment(File);

            message.Save();

            message.Send();
        }

        public void SendMails(List<string> MailTo, string MailTitle, string MailBody)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailSender, EmailDisplay);
            foreach (var item in MailTo) mail.To.Add(item);
            mail.Subject = MailTitle;
            mail.IsBodyHtml = true;
            mail.Body = MailBody;

            var smtp = new SmtpClient
            {
                Host = EmailHost,
                Port = EmailPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailUser, EmailPassword)
            };

            smtp.Send(mail);
        }

        public void SendMails(List<string> MailTo, string MailTitle, string MailBody, string File)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailSender, EmailDisplay);
            foreach (var item in MailTo) mail.To.Add(item);
            mail.Subject = MailTitle;
            mail.Attachments.Add(new System.Net.Mail.Attachment(File));
            mail.IsBodyHtml = true;
            mail.Body = MailBody;

            var smtp = new SmtpClient
            {
                Host = EmailHost,
                Port = EmailPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailSender, EmailPassword)
            };

            smtp.Send(mail);
        }

        public void SendPush(List<int> UserIDs, string Title, string Body)
        {
            foreach (var item in new BusinessDevicePhoneUser().GetByUserIDs(UserIDs))
            {
                new BusinessGoogle().SendNotification(item.FCM, Title, Body);
            }            
        }

        public ModelViewNotification Insert(ModelViewNotification model)
        {
         
            EntityNotification data = new EntityNotification() {
                MessageID = model.MessageID,
                Message = model.Message,
                MessageRead = model.MessageRead,
                Title = model.Title,
                Url = model.Url,
                UserID = model.UserID,
                Status = true,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow
            };

            data = new RepositoryNotification().Insert(data);
            model.MessageID = data.MessageID;

            return model;
        }

        public ModelViewNotification Update(ModelViewNotification model)
        {
            EntityNotification data = new EntityNotification()
            {
                MessageID = model.MessageID,
                Message = model.Message,
                MessageRead = model.MessageRead,
                Title = model.Title,
                Url = model.Url,
                UserID = model.UserID,
                Status = model.Status,
                CreateDate = model.CreateDate,
                ModifyDate = DateTime.UtcNow
            };

            data = new RepositoryNotification().Update(data);
            return model;
        }

        public void Status(ModelViewNotificationUpdate model)
        {
            if (model.TokenApp != GlobalConfiguration.TokenWEB)
                if (model.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            var data = new RepositoryNotification().Get(model.MessageID);
            data.MessageRead = model.MessageRead;
            data.Status = model.Status;
            new RepositoryNotification().Update(data);

        }

        public List<ModelViewNotification> GetAll()
        {
            return new RepositoryNotification().GetAll().Select(p => new ModelViewNotification() {
                MessageID = p.MessageID,
                Message = p.Message,
                MessageRead = p.MessageRead,
                Title = p.Title,
                Url = p.Url,
                UserID = p.UserID,
                UserName = p.UserName,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
                CreateDateString = p.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                ModifyDateString = p.ModifyDate.ToString("dd/MM/yyyy HH:mm:ss")
            }).OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<ModelViewNotification> GetAllActives()
        {
            return new RepositoryNotification().GetActives().Select(p => new ModelViewNotification()
            {
                MessageID = p.MessageID,
                Message = p.Message,
                MessageRead = p.MessageRead,
                Title = p.Title,
                Url = p.Url,
                UserID = p.UserID,
                UserName = p.UserName,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
                CreateDateString = p.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                ModifyDateString = p.ModifyDate.ToString("dd/MM/yyyy HH:mm:ss")
            }).OrderByDescending(p => p.CreateDate).ToList();
        }

        public ModelViewNotification Get(ModelViewNotificationUpdate model)
        {
            if (model.TokenApp != GlobalConfiguration.TokenWEB)
                if (model.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            var data = new RepositoryNotification().Get(model.MessageID);

            return new ModelViewNotification()
            {
                MessageID = data.MessageID,
                Message = data.Message,
                MessageRead = data.MessageRead,
                Title = data.Title,
                Url = data.Url,
                UserID = data.UserID,
                UserName = data.UserName,
                Status = data.Status,
                CreateDate = data.CreateDate,
                ModifyDate = data.ModifyDate,
                CreateDateString = data.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                ModifyDateString = data.ModifyDate.ToString("dd/MM/yyyy HH:mm:ss")
            };
        }

        public List<ModelViewNotification> GetByToken(EntitySecurity model)
        {
            if (model.TokenApp != GlobalConfiguration.TokenWEB)
                if (model.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            var dataUser = new BusinessUsers().GetUserByToken(model.TokenUser);

            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

            return new RepositoryNotification().GetByUserID(dataUser.UserID).Select(p => new ModelViewNotification()
            {
                MessageID = p.MessageID,
                Message = p.Message,
                MessageRead = p.MessageRead,
                Title = p.Title,
                Url = p.Url,
                UserID = p.UserID,
                UserName = p.UserName,
                Status = p.Status,
                CreateDate = TimeZoneInfo.ConvertTimeFromUtc(p.CreateDate, estZone),
                ModifyDate = TimeZoneInfo.ConvertTimeFromUtc(p.ModifyDate, estZone),
                CreateDateString = TimeZoneInfo.ConvertTimeFromUtc(p.CreateDate, estZone).ToString("dd/MM/yyyy HH:mm:ss"),
                ModifyDateString = TimeZoneInfo.ConvertTimeFromUtc(p.ModifyDate, estZone).ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();
        }

        public List<ModelViewNotification> GetByTokenActive(string TokenUser)
        {
            var dataUser = new BusinessUsers().GetUserByToken(TokenUser);

            return new RepositoryNotification().GetAll().Where(p => p.UserID == dataUser.UserID && p.Status == true).Select(p => new ModelViewNotification()
            {
                MessageID = p.MessageID,
                Message = p.Message,
                MessageRead = p.MessageRead,
                Title = p.Title,
                Url = p.Url,
                UserID = p.UserID,
                UserName = p.UserName,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
                CreateDateString = p.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                ModifyDateString = p.ModifyDate.ToString("dd/MM/yyyy HH:mm:ss")
            }).OrderByDescending(p => p.CreateDate).ToList();
        }
        
        public List<ModelViewUserNotification> GetAllNotificationUsers(string ModuleID, string Name)
        {
            var NegocioUsuario = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioModulosMabe = new BusinessModuleService();
           // var usuarios = NegocioUsuario.GetActives().Where(p=> p.ProfileID == 4);
            var empleados = NegocioEmpleado.GetAll();
            var modulos = new List<EntityModuleService>();
            var usuarios = new List<ModelViewUserList>();

            if (ModuleID != null)
            {
                int[] nums = ModuleID.Split(',').Select(int.Parse).ToArray();
                modulos = NegocioModulosMabe.GetAll().Where(p => nums.Contains(p.ModuleID)).ToList();
            }
            else
            { modulos = NegocioModulosMabe.GetAll(); }

            if (Name != "") 
            { usuarios = NegocioUsuario.GetActives().Where(p => p.ProfileID == 4 && (p.Name.ToLower().Contains(Name.ToLower()) || p.UserName.Contains(Name))).ToList(); }
            else
            { usuarios = NegocioUsuario.GetActives(); }

            return (from a in usuarios
                      join b in empleados on a.UserID equals b.FK_UserID
                      join c in modulos on b.FK_ModuleID equals c.ModuleID
                      select new ModelViewUserNotification()
                      {
                          UserID = a.UserID,
                          Name ="(" +a.UserName+ ")" + " " + b.FirstName + " " + b.LastName + " " + c.ID + " - " + c.Base
                      }).ToList();
        }
        
        public List<ModelViewUserNotification> GetAllNotificationUsers(string Employee)
        {
            var NegocioUsuario = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioModulosMabe = new BusinessModuleService();
            var empleados = NegocioEmpleado.GetAll();
            var modulos = new List<EntityModuleService>();
            var usuarios = new List<ModelViewUserList>();
            modulos = NegocioModulosMabe.GetAll();
   
                int[] nums = Employee.Split(',').Select(int.Parse).ToArray();
                usuarios = NegocioUsuario.GetActives().Where(p => nums.Contains(p.UserID)).ToList();
   
            return (from a in usuarios
                    join b in empleados on a.UserID equals b.FK_UserID
                    join c in modulos on b.FK_ModuleID equals c.ModuleID
                    select new ModelViewUserNotification()
                    {
                        UserID = a.UserID,
                        Name = "(" + a.UserName + ")" + " " + b.FirstName + " " + b.LastName + " " + c.ID + " - " + c.Base
                    }).ToList();
        }
        

    }
}
