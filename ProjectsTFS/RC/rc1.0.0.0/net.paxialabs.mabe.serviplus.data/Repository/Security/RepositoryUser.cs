/*
Nombre del programa:            Serviplus v1.0
Nombre del componente:          RepositoryUser
Creado por :                    Miguel Angel Ordoñez
Fecha de creación:              02/05/2017
Ultima modificación:            02/05/2017
Modificado por :                
Versión:                        1.0
Descripción:                    Consulta de Usuario por
                                    Email
                                    Token
                                    Usuario
                                    ID
                                    Activos
                                    Todos
                                Insertar Usuario
                                Actualizar Usuario
                                
                                
Histórico de modificación:      
*/

using net.paxialabs.mabe.serviplus.data.Factory.Security;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;


namespace net.paxialabs.mabe.serviplus.data.Repository.Security
{
    public class RepositoryUser : BaseRepository, IRepositoryGET<EntityUser>, IRepositorySET<EntityUser>
    {
        public EntityUser GetEmail(string email)
        {
            var data = base.DataContext.User.Where(p => p.Email == email);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }

        public EntityUser GetToken(string token)
        {
            var data = base.DataContext.User.Where(p => p.Token == token);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }

        public EntityUser GetUserName(string userName)
        {
            var data = base.DataContext.User.Where(p => p.UserName == userName);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }

        public EntityUser Get(int Id)
        {
            var data = base.DataContext.User.Where(p => p.UserID == Id);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }
       

        public EntityUser GetUserByToken(string Token)
        {
            var data = base.DataContext.User.Where(p => p.Token == Token);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }
        
        public EntityUser GetProfileByToken(string Token)
        {
            var data = base.DataContext.User.Where(p => p.Token == Token);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }

        public EntityUser GetUserByEmail(string Email)
        {
            var data = base.DataContext.User.Where(p => p.Email == Email);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }
        public EntityUser GetByID(int UserID)
        {
            var data = base.DataContext.User.Where(p => p.UserID == UserID);
            if (data.Count() == 1)
                return FactoryUser.Get(data.Single());
            else
                return null;
        }


        public List<EntityUser> GetActives()
        {
            return FactoryUser.GetList(base.DataContext.User.Where(p => p.Status == true).ToList());
        }

        public List<EntityUser> GetByIDs(List<int> IDS)
        {
            return FactoryUser.GetList(base.DataContext.User.Where(p => IDS.Contains(p.UserID)).ToList());
        }

        public List<EntityUser> GetAll()
        {
            return FactoryUser.GetList(base.DataContext.User.ToList());
        }
        public List<EntityUser> GetAllByModule(int module)
        {
            return FactoryUser.GetList(base.DataContext.User.Where(p =>p.FK_ModuleID==module).ToList());
        }
        public List<EntityUser> GetAllUserModuleProfile(int module, int profile)
        {
            return FactoryUser.GetList(base.DataContext.User.Where(p => p.FK_ModuleID == module && p.ProfileID== profile).ToList());
        }
        public List<EntityUser> GetAllByID(int UserID)
        {
            return FactoryUser.GetList(base.DataContext.User.Where(p => p.UserID== UserID).ToList());
        }

        public EntityUser Insert(EntityUser data)
        {
            try
            {
                User dataNew = new User()
                {
                    UserID = data.UserID,
                    ProfileID = data.ProfileID,
                    Name = data.Name,
                    UserName = data.UserName,
                    Email = data.Email,
                    Token = data.Token,
                    Password = data.Password,
                    ChangePassword = data.ChangePassword,
                    Status = data.Status,
                    DateCreate = data.DateCreate,
                    DateModification = data.DateModification,
                    DateLastAccess = data.DateLastAccess
                };
                base.DataContext.User.Add(dataNew);
                base.DataContext.SaveChanges();

                data.UserID = dataNew.UserID;

                return data;
            }
            catch (DbException dbex)
            {
                throw dbex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityUser Update(EntityUser data)
        {
            try
            {
                var dataUpdate = base.DataContext.User.Where(p => p.UserID == data.UserID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.ChangePassword = data.ChangePassword;
                    dataUpdate.Password = data.Password;
                    dataUpdate.Email = data.Email;
                    dataUpdate.Status = data.Status;
                    dataUpdate.DateCreate = data.DateCreate;
                    dataUpdate.DateModification = data.DateModification;
                    dataUpdate.DateLastAccess = data.DateLastAccess;
                    dataUpdate.Name = data.Name;
                    dataUpdate.ProfileID = data.ProfileID;
                    dataUpdate.Token = data.Token;
                    dataUpdate.UserName = data.UserName;
                    dataUpdate.UserID = data.UserID;    
                    
                    base.DataContext.Entry(dataUpdate).State = EntityState.Modified;
                    base.DataContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el registro en la base de datos a modificar.");
                }

                return data;
            }
            catch (DbException dbex)
            {
                throw dbex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
