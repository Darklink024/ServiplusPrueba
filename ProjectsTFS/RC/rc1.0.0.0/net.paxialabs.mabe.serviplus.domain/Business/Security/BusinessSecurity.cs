using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Notification;
using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.resource.Common;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    internal class BusinessSecurity
    {
        char[] ValueAfanumeric = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',
                                   'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z',
                                   'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R',
                                   'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F',
                                   'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B',
                                   'N', 'M'};  //, '!', '#', '$', '%', '&', '?', '¿' };

        private ResourceManager ResourceMessage;

        public BusinessSecurity() {
            ResourceMessage = new ResourceManager("net.paxialabs.mabe.serviplus.resource.Common.ResourceMessage", Assembly.GetExecutingAssembly());            
        }

        public ModelViewUser Authenticate(ModelViewLogin model)
        {
            ModelViewUser result = null;

            var dataUsuario = new RepositoryUser().GetUserName(model.UserName);

            if (model.Token != GlobalConfiguration.TokenWEB)
                if (model.Token != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            if (dataUsuario.ProfileID == 4 && model.Origin == "WEB") throw new Exception("NoAccess");

            var x = new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(model.Password);
            if (dataUsuario.Password != new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(model.Password)) throw new Exception("UserPasswordInvalid");

            if (!dataUsuario.Status) throw new Exception("UserInvalid");
            if (dataUsuario.ModuleID != null)
            {
                var module = new BusinessModuleService().GetAllBYModule(dataUsuario.ModuleID.Value);
                result = new ModelViewUser()
                {
                    UserID = dataUsuario.UserID,
                    Name = dataUsuario.Name,
                    Token = dataUsuario.Token,
                    ProfileID = dataUsuario.ProfileID,
                    ChangePassword = dataUsuario.ChangePassword,
                    Email = dataUsuario.Email,
                    UserName = dataUsuario.UserName,
                    Profile = new RepositoryProfile().Get(dataUsuario.ProfileID).Profile,
                    EmployeeStore = new BusinessEmployee().GetEmployeeStore(dataUsuario.UserID),
                    LatWorkshop = module.LatWorkshop,
                    LongWorkshop = module.LongWorkshop


                };
            }
            else {
                
                result = new ModelViewUser()
                {
                    UserID = dataUsuario.UserID,
                    Name = dataUsuario.Name,
                    Token = dataUsuario.Token,
                    ProfileID = dataUsuario.ProfileID,
                    ChangePassword = dataUsuario.ChangePassword,
                    Email = dataUsuario.Email,
                    UserName = dataUsuario.UserName,
                    Profile = new RepositoryProfile().Get(dataUsuario.ProfileID).Profile,
                    EmployeeStore = new BusinessEmployee().GetEmployeeStore(dataUsuario.UserID),
                    LatWorkshop =0 ,
                    LongWorkshop =0 



                };
            }
            
            
                       
            return result;
        }

        public ModelViewUser Authenticate(string UserName, string Password)
        {
            ModelViewUser result = null;

            var dataUsuario = new RepositoryUser().GetUserName(UserName);
            
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            if (dataUsuario.Password != new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(Password)) throw new Exception("UserPasswordInvalid");

            if (!dataUsuario.Status) throw new Exception("UserInvalid");

            result = new ModelViewUser()
            {
                UserID = dataUsuario.UserID,
                Name = dataUsuario.Name,
                Token = dataUsuario.Token,
                ProfileID = dataUsuario.ProfileID,
                ChangePassword = dataUsuario.ChangePassword,
                Email = dataUsuario.Email,
                UserName = dataUsuario.UserName,
                Profile = new RepositoryProfile().Get(dataUsuario.ProfileID).Profile,
                EmployeeStore = new BusinessEmployee().GetEmployeeStore(dataUsuario.UserID)
            };

            return result;
        }

        public void Recovery(ModelViewRecovery model)
        {
            var objRepo = new RepositoryUser();
            var dataUsuario = objRepo.GetEmail(model.Email);

            if (dataUsuario == null) throw new Exception("EmailInvalid");

            var NewPassword = GeneratePassword(8);

            dataUsuario.Password = new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(NewPassword);
            dataUsuario.ChangePassword = true;

            objRepo.Update(dataUsuario);

            List<string> arr = new List<string>();
            arr.Add(model.Email);
            //arr.Add("juan.peralta@paxialabs.com");

            var objAlerta = new BusinessNotification();

            //byte[] outMessage = new byte[ResourceMessage.GetStream("NotificationRecoveryBodyContent", culture).Length];
            //ResourceMessage.GetStream("NotificationRecoveryBodyContent", culture).Read(outMessage, 0, (int) ResourceMessage.GetStream("NotificationRecoveryBodyContent", culture).Length);

            //StringBuilder sb = new StringBuilder(UnicodeEncoding.Unicode.GetString(outMessage));
            string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationRecoveryBodyContent.txt");
            sb = sb.Replace("#%Nombre%#", dataUsuario.Name);
            sb = sb.Replace("#%ClaveTemporal%#", NewPassword);
            var Manager = new ResourceManager("net.paxialabs.mabe.serviplus.resource.Common.ResourceMessage", typeof(ResourceMessage).Assembly);
            //objAlerta.SendMails(arr, Manager.GetString("NotificationRecoveryPassword"), sb.ToString());
            objAlerta.SendMailExchange(GlobalConfiguration.exchangeUser, GlobalConfiguration.exchangePwd, arr, Manager.GetString("NotificationRecoveryPassword"), sb.ToString());
        }

        public void ResetPassword(int ID)
        {
            var objRepo = new RepositoryUser();
            var dataUsuario = objRepo.Get(ID);

            if (dataUsuario == null) throw new Exception("UserInvalid");

            var NewPassword = GeneratePassword(8);

            dataUsuario.Password = new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(NewPassword);
            dataUsuario.ChangePassword = true;

            objRepo.Update(dataUsuario);

            List<string> arr = new List<string>();
            arr.Add(dataUsuario.Email);

            var objAlerta = new BusinessNotification();

            byte[] outMessage = new byte[ResourceMessage.GetStream("NotificationRecoveryBodyContent").Length];
            ResourceMessage.GetStream("NotificationRecoveryBodyContent").Read(outMessage, 0, (int)ResourceMessage.GetStream("NotificationRecoveryBodyContent").Length);

            StringBuilder sb = new StringBuilder(UnicodeEncoding.Unicode.GetString(outMessage));

            sb = sb.Replace("#%Nombre%#", dataUsuario.Name);
            sb = sb.Replace("#%ClaveTemporal%#", NewPassword);

            //objAlerta.SendMails(arr, ResourceMessage.GetString("NotificationRecoveryPassword"), sb.ToString());
            objAlerta.SendMailExchange(GlobalConfiguration.exchangeUser, GlobalConfiguration.exchangePwd, arr, ResourceMessage.GetString("NotificationRecoveryPassword"), sb.ToString());
        }

        public void ChangePassword(ModelViewChangePassword model)
        {
            if (model.TokenApp != GlobalConfiguration.TokenWEB)
                if (model.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            var objRepo = new RepositoryUser();
            var dataUsuario = objRepo.GetToken(model.TokenUser);

            if (dataUsuario.Password != new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(model.OldPassword))
                throw new Exception("PasswordOldInvalid");

            if (model.NewPassword != model.ConfirmPassword) throw new Exception("ConfirmPassword");

            dataUsuario.Password = new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(model.NewPassword);
            dataUsuario.ChangePassword = false;

            objRepo.Update(dataUsuario);
        }

        public string GeneratePassword(int LongPassMax)
        {
            string Password = String.Empty;
            try
            {
                Random ram = new Random();

                for (int i = 0; i < LongPassMax; i++)
                {
                    int rm = ram.Next(0, 2);

                    if (rm == 0)
                    {
                        Password += ram.Next(0, 10);
                    }
                    else
                    {
                        Password += ValueAfanumeric[ram.Next(0, 52)];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Password;
        }

        public string GenerateNIP(string Prefix, int LongPassMax, bool EsAlfanumerico = true)
        {
            string Password = String.Empty;
            try
            {
                Random ram = new Random(Guid.NewGuid().GetHashCode());

                for (int i = 0; i < LongPassMax; i++)
                {
                    int rm = ram.Next(0, 2);

                    if (rm == 0)
                    {
                        Password += ram.Next(0, 10);
                    }
                    else
                    {
                        Password += EsAlfanumerico ? ValueAfanumeric[ram.Next(0, 52)] : ram.Next(0, 10);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Prefix + Password;
        }

        public string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public List<EntityLogMobile> GEtBilling(string DateIn,string DateFn, string Message)
        {
            DateTime DateIns = Convert.ToDateTime(DateIn);
            DateTime DateFns = Convert.ToDateTime(DateFn);
            
            return new RepositoryLogMobile().GetBillings(DateIns, DateFns, Message);

            

        }

    }
}
