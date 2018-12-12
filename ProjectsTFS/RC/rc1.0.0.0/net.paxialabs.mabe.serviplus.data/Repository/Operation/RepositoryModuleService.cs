using net.paxialabs.mabe.serviplus.data.Factory.Operation;
using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Repository.Operation
{
    public class RepositoryModuleService : BaseRepository, IRepositoryGET<EntityModuleService>, IRepositorySET<EntityModuleService>
    {
        public EntityModuleService Get(int Id)
        {
            var data = base.DataContext.ModuleMabe.Where(p => p.PK_ModuleID == Id);
            if (data.Count() == 1)
                return FactoryModuleService.Get(data.Single());
            else
                return null;
        }

        public List<EntityModuleService> GetActives()
        {
            return FactoryModuleService.GetList(base.DataContext.ModuleMabe.Where(p => p.Status == true).ToList());
        }

        public List<EntityModuleService> GetAll()
        {
            return FactoryModuleService.GetList(base.DataContext.ModuleMabe.ToList());
        }
        public EntityModuleService GetAllBYModule(int module)
        {
            var data = base.DataContext.ModuleMabe.Where(p => p.PK_ModuleID == module);
            if (data.Count() == 1)
                return FactoryModuleService.Get(data.Single());
            else
                return null;
        }

        public List<EntityModuleService> GetAllBYModuleList(int module)
        {

            return FactoryModuleService.GetList(base.DataContext.ModuleMabe.Where(p => p.PK_ModuleID == module).ToList());


        }
        public EntityModuleService Insert(EntityModuleService data)
        {
            try
            {
                ModuleMabe dataNew = new ModuleMabe()
                {
                    PK_ModuleID = data.ModuleID,
                    ModuleID = data.ID,
                    Module = data.Module,
                    Base = data.Base,
                    Region = data.Region,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.ModuleMabe.Add(dataNew);
                base.DataContext.SaveChanges();

                data.ModuleID = dataNew.PK_ModuleID;

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

        public EntityModuleService Update(EntityModuleService data)
        {
            try
            {
                var dataUpdate = base.DataContext.ModuleMabe.Where(p => p.PK_ModuleID == data.ModuleID).SingleOrDefault();

                if (dataUpdate != null)
                {

                    dataUpdate.PK_ModuleID = data.ModuleID;
                    dataUpdate.ModuleID = data.ID;
                    dataUpdate.Module = data.Module;
                    dataUpdate.Base = data.Base;
                    dataUpdate.Region = data.Region;
                    dataUpdate.Status = data.Status;
                    dataUpdate.CreateDate = data.CreateDate;
                    dataUpdate.ModifyDate = data.ModifyDate;

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
