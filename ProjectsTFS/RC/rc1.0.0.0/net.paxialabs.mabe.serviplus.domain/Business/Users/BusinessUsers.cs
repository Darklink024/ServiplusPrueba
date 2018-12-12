using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Notification;
using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Users
{
    internal class BusinessUsers
    {

        public ModelViewUser Get(int UserID)
        {
            ModelViewUser result = new ModelViewUser();
            var data = new RepositoryUser().Get(UserID);
            result.Email = data.Email;
            result.Name = data.Name;
            result.ProfileID = data.ProfileID;
            result.FK_ModuleID = data.ModuleID;
            result.UserID = data.UserID;
            result.UserName = data.UserName;
            return result;
        }



        public List<ModelViewUserList> GetAll()
        {
            return new RepositoryUser().GetAll().Select(p => new ModelViewUserList()
            {
                UserID = p.UserID,
                ProfileID = p.ProfileID,
                UserName = p.UserName,
                FK_ModuleID=p.ModuleID,
                Name = p.Name,
                Email = p.Email,
                Status = p.Status,
                DateCreate = p.DateCreate,
                DateLastAccess = p.DateLastAccess,
                DateModification = p.DateModification,
            }).ToList<ModelViewUserList>();
        }
        public List<ModelViewUserList> GetAllUserModuleProfile(int module, int profile)
        {
            return new RepositoryUser().GetAllUserModuleProfile(module, profile).Select(p => new ModelViewUserList()
            {
                UserID = p.UserID,
                ProfileID = p.ProfileID,
                UserName = p.UserName,
                FK_ModuleID = p.ModuleID,
                Name = p.Name,
                Email = p.Email,
                Status = p.Status,
                DateCreate = p.DateCreate,
                DateLastAccess = p.DateLastAccess,
                DateModification = p.DateModification,
            }).ToList<ModelViewUserList>();
        }
        
        public List<ModelViewUsers> GetAllList()
        {
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioModulos = new BusinessModuleService();
            var usuarios = GetAll();
            var empleados = NegocioEmpleado.GetAll();
            var modulos = NegocioModulos.GetAll();
            var lt = (from a in usuarios
                      join b in empleados on a.UserID equals b.FK_UserID
                      join c in modulos on b.FK_ModuleID equals c.ModuleID
                      select new ModelViewUsers()
                      {
                          UserID = a.UserID,
                          ProfileID = a.ProfileID,
                          UserName = a.UserName,
                          Name = a.Name,
                          Email = a.Email,
                          Status = a.Status,
                          DateCreate = a.DateCreate,
                          DateLastAccess = a.DateLastAccess,
                          DateModification = a.DateModification,
                          EmployyeID = b.EmployeeID,
                          Module = c.ID + " - " + c.Base
                      }).ToList();
            return lt;
        }

        public List<ModelViewUserList> GetActives()
        {
            return new RepositoryUser().GetActives().Select(p => new ModelViewUserList()
            {
                UserID = p.UserID,
                ProfileID = p.ProfileID,
                UserName = p.UserName,
                Name = p.Name,
                Email = p.Email,
                Status = p.Status,
                DateCreate = p.DateCreate,
                DateLastAccess = p.DateLastAccess,
                DateModification = p.DateModification
            }).ToList<ModelViewUserList>();
        }

        public ModelViewUser Insert(ModelViewUser model)
        {
            var objRepository = new RepositoryUser();
            var objSecurity = new BusinessSecurity();

            bool salir = false;
            string token = "";

            while (!salir)
            {
                token = objSecurity.GenerateToken();
                if (objRepository.GetToken(token) == null) salir = true;
            }
            string clave = objSecurity.GeneratePassword(8);

            if (objRepository.GetEmail(model.Email) != null) throw new Exception("DuplicateEmail");

            if (objRepository.GetUserName(model.UserName) != null) throw new Exception("DuplicateUser");


            EntityUser data = new EntityUser()
            {
                UserID = model.UserID,
                ProfileID = model.ProfileID,
                UserName = model.UserName,
                Name = model.Name,
                Password = new BusinessCryptoMD5(GlobalConfiguration.CryptoKey).CryptoString(clave),
                Token = token,
                ChangePassword = true,
                Email = model.Email,
                Status = true,
                DateLastAccess = DateTime.UtcNow,
                DateCreate = DateTime.UtcNow,
                DateModification = DateTime.UtcNow
            };

            data = objRepository.Insert(data);

            model.UserID = data.UserID;

            List<string> arr = new List<string>();
            arr.Add(model.Email);
      
            string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationUserBodyContent.txt");
            sb = sb.Replace("#%NombreUsuario%#", data.UserName);
            sb = sb.Replace("#%ClaveTemporal%#", clave);
            sb = sb.Replace("#%Nombre%#", data.Name);
            //Nombre de usuario, usuario, pass,
            //new BusinessNotification().SendMails(arr, "Mabe - Registro de usuario ServiPlus", sb);
            new BusinessNotification().SendMailExchange(GlobalConfiguration.exchangeUser, GlobalConfiguration.exchangePwd, arr, "Mabe - Registro de usuario ServiPlus", sb);
            return model;
        }

        public ModelViewUser Update(ModelViewUser model)
        {
            var objRepository = new RepositoryUser();

            var dataOld = new RepositoryUser().Get(model.UserID);

            EntityUser data = new EntityUser()
            {
                UserID = model.UserID,
                ProfileID = model.ProfileID,
                UserName = model.UserName,
                Name = model.Name,
                Password = dataOld.Password,
                Token = dataOld.Token,
                ChangePassword = dataOld.ChangePassword,
                Email = model.Email,
                Status = dataOld.Status,
                DateLastAccess = dataOld.DateLastAccess,
                DateCreate = dataOld.DateCreate,
                DateModification = DateTime.UtcNow
            };

            data = objRepository.Update(data);

            return model;
        }

        public void SetStatus(List<int> IDs)
        {
            foreach (var item in IDs)
            {
                var data = new RepositoryUser().Get(item);
                data.Status = !data.Status;
                data.DateModification = DateTime.UtcNow;
                new RepositoryUser().Update(data);
            }
        }

        public EntityUser GetUserByToken(string Token)
        {
            EntityUser result = new EntityUser();
            var data = new RepositoryUser().GetUserByToken(Token);
            if (data == null) return null;
            result.ModuleID = data.ModuleID;
            result.Email = data.Email;
            result.Name = data.Name;
            result.ProfileID = data.ProfileID;
            result.UserID = data.UserID;
            result.UserName = data.UserName;
            return result;
        }
        public EntityUser GetProfileByToken(string Token)
        {
            EntityUser result = new EntityUser();
            var data = new RepositoryUser().GetProfileByToken(Token);
            result.ModuleID = data.ModuleID;
            result.Email = data.Email;
            result.Name = data.Name;
            result.ProfileID = data.ProfileID;
            result.UserID = data.UserID;
            result.UserName = data.UserName;
            return result;
        }
        public List<EntityUser> GetUserByModule(int ModuleID)
        {
            return new RepositoryUser().GetAllByModule(ModuleID).Select(p => new EntityUser()
            {
                UserID = p.UserID,
                ProfileID = p.ProfileID,
                UserName = p.UserName,
                ModuleID = p.ModuleID,
                Name = p.Name,
                Email = p.Email,
                Status = p.Status,
                DateCreate = p.DateCreate,
                DateLastAccess = p.DateLastAccess,
                DateModification = p.DateModification,
            }).ToList<EntityUser>();
        }
        public EntityUser GetUserByEmail(string Email)
        {
            EntityUser result = new EntityUser();
            var data = new RepositoryUser().GetUserByEmail(Email);
            result.Email = data.Email;
            result.Name = data.Name;
            result.ProfileID = data.ProfileID;
            result.UserID = data.UserID;
            result.UserName = data.UserName;
            return result;
        }
        public EntityUser GetByID(int UserID)
        {
            EntityUser result = new EntityUser();
            var data = new RepositoryUser().GetByID(UserID);
            result.Email = data.Email;
            result.Name = data.Name;
            result.ProfileID = data.ProfileID;
            result.Token = data.Token;
            result.UserID = data.UserID;
            result.UserName = data.UserName;
            return result;
        }

        public void RegisterFCM(ModelViewUserFCM model)
        {
            if (model.TokenApp != GlobalConfiguration.TokenWEB)
                if (model.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            var objRepo = new RepositoryUser();

            var dataUsuario = objRepo.GetToken(model.TokenUser);
            var dataDevice = new BusinessDevicePhone().GetByIMEI(model.IMEI);
                        
            if (dataDevice != null)
            {
                dataDevice.Phone = model.Phone;
                dataDevice.FCM = model.TokenPush;
                new BusinessDevicePhone().Update(dataDevice);
                              
            }
            else
            {
                dataDevice = new ModelViewDevicePhone()
                {
                    DevicePhoneID = 0,
                    IMEI = model.IMEI,
                    Phone = model.Phone,
                    FCM = model.TokenPush,
                    Status = true,
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow
                };

                dataDevice = new BusinessDevicePhone().Register(dataDevice);
            }

            foreach (var dataDeviceUser in new BusinessDevicePhoneUser().GetByUser(dataUsuario.UserID))
            {
                dataDeviceUser.Status = false;
                dataDeviceUser.ModifyDate = DateTime.UtcNow;
                new BusinessDevicePhoneUser().Update(dataDeviceUser);
            }

            foreach (var dataDeviceUser in new BusinessDevicePhoneUser().GetByDevicePhone(dataDevice.DevicePhoneID))
            {
                dataDeviceUser.Status = false;
                dataDeviceUser.ModifyDate = DateTime.UtcNow;
                new BusinessDevicePhoneUser().Update(dataDeviceUser);
            }

            var dataUserDevice = new BusinessDevicePhoneUser().Get(dataDevice.DevicePhoneID, dataUsuario.UserID);

            if (dataUserDevice == null)
            {
                new BusinessDevicePhoneUser().Insert(new entities.ModelView.Security.ModelViewDevicePhoneUser()
                {
                    UserID = dataUsuario.UserID,
                    DevicePhoneID = dataDevice.DevicePhoneID,
                    FCM = model.TokenPush,
                    Status = true,
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow
                });

            }
            else
            {
                dataUserDevice.Status = true;
                dataUserDevice.ModifyDate = DateTime.UtcNow;
                new BusinessDevicePhoneUser().Update(dataUserDevice);
            }
                        
        }

        public List<EntityUser> GetByIDs(List<int> IDS)
        {
            return new RepositoryUser().GetByIDs(IDS);
        }
    }
}
