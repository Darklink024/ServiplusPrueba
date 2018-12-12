using net.paxialabs.mabe.serviplus.data.Factory.Security;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Security
{
    public class RepositoryPermission : BaseRepository, IRepositoryGET<EntityPermission>, IRepositorySET<EntityPermission>
    {
        public EntityPermission Get(int Id)
        {
            throw new NotImplementedException();
        }

        public EntityPermission Get(int ModuleID, int ProfileID)
        {
            var data = base.DataContext.Permission.Where(p => p.ModuleID == ModuleID & p.ProfileID == ProfileID);
            if (data.Count() == 1)
                return FactoryPermission.Get(data.Single());
            else
                return null;
        }
        
        public List<EntityPermission> GetActives()
        {
            throw new NotImplementedException();
        }

        public List<EntityPermission> GetAll()
        {
            return FactoryPermission.GetList(base.DataContext.Permission.ToList());
        }

        public List<EntityPermission> GetAll(int ProfileID)
        {
            return FactoryPermission.GetList(base.DataContext.Permission.Where(p => p.ProfileID == ProfileID).ToList());
        }

        public EntityPermission Insert(EntityPermission data)
        {
            try
            {
                Permission dataNew = new Permission()
                {
                    ModuleID = data.ModuleID,
                    ProfileID = data.ProfileID,
                    Access = data.Access,
                    Add = data.Add,
                    Delete = data.Delete,
                    Export = data.Export,
                    Read = data.Read,
                    Update = data.Update
                };
                base.DataContext.Permission.Add(dataNew);
                base.DataContext.SaveChanges();

                data.ModuleID = dataNew.ModuleID;

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

        public EntityPermission Update(EntityPermission data)
        {
            try
            {
                var dataUpdate = base.DataContext.Permission.Where(p => p.ModuleID == data.ModuleID & p.ProfileID == data.ProfileID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    dataUpdate.ModuleID = data.ModuleID;
                    dataUpdate.ProfileID = data.ProfileID;
                    dataUpdate.Access = data.Access;
                    dataUpdate.Add = data.Add;
                    dataUpdate.Delete = data.Delete;
                    dataUpdate.Export = data.Export;
                    dataUpdate.Read = data.Read;
                    dataUpdate.Update = data.Update;

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
