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
    public class RepositoryModule : BaseRepository, IRepositoryGET<EntityModule>, IRepositorySET<EntityModule>
    {
        public EntityModule Get(int Id)
        {
            var data = base.DataContext.Module.Where(p => p.ModuleID == Id);
            if (data.Count() == 1)
                return FactoryModule.Get(data.Single());
            else
                return null;
        }

        public EntityModule GetURL(string URL)
        {
            var data = base.DataContext.Module.Where(p => p.URL == URL);
            if (data.Count() == 1)
                return FactoryModule.Get(data.Single());
            else
                return null;
        }

        public List<EntityModule> GetActives()
        {
            return FactoryModule.GetList(base.DataContext.Module.Where(p => p.Status == true).ToList());
        }

        public List<EntityModule> GetAll()
        {
            return FactoryModule.GetList(base.DataContext.Module.ToList());
        }

        public EntityModule Insert(EntityModule data)
        {
            try
            {
                Module dataNew = new Module()
                {                    
                    ModuleID = data.ModuleID,
                    Module1 = data.Module,
                    Description = data.Description,
                    Section = data.Section,
                    URL = data.URL,
                    Status = data.Status
                };
                base.DataContext.Module.Add(dataNew);
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

        public EntityModule Update(EntityModule data)
        {
            try
            {
                var dataUpdate = base.DataContext.Module.Where(p => p.ModuleID == data.ModuleID).SingleOrDefault();

                if (dataUpdate != null)
                {                    
                    dataUpdate.ModuleID = data.ModuleID;
                    dataUpdate.Module1 = data.Module;
                    dataUpdate.Description = data.Description;
                    dataUpdate.Section = data.Section;
                    dataUpdate.Status = data.Status;
                    dataUpdate.URL = data.URL;
                   

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
