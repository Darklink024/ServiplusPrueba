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
   public class RepositoryPriority : BaseRepository, IRepositoryGET<EntityPriority>, IRepositorySET<EntityPriority>
    {
        public EntityPriority Get(int Id)
        {
            var data = base.DataContext.Priority.Where(p => p.PK_PriorityID == Id);
            if (data.Count() == 1)
                return FactoryPriority.Get(data.Single());
            else
                return null;
        }

        public List<EntityPriority> GetActives()
        {
            return FactoryPriority.GetList(base.DataContext.Priority.Where(p => p.Status == true).ToList());
        }

        public List<EntityPriority> GetAll()
        {
            return FactoryPriority.GetList(base.DataContext.Priority.ToList());
        }

        public EntityPriority Insert(EntityPriority data)
        {
            try
            {
                Priority dataNew = new Priority()
                {
                    PK_PriorityID = data.PK_PriorityID,
                    FK_ScheduleID = data.FK_ScheduleID,
                    Priority1 = data.Priority1,
                    Description = data.Description,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Priority.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_PriorityID = dataNew.PK_PriorityID;

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

        public EntityPriority Update(EntityPriority data)
        {
            try
            {
                var dataUpdate = base.DataContext.Priority.Where(p => p.PK_PriorityID == data.PK_PriorityID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    //dataUpdate.PK_PriorityID = data.PK_PriorityID;
                    //dataUpdate.FK_ScheduleID = data.FK_ScheduleID;
                    dataUpdate.Priority1 = data.Priority1;
                    dataUpdate.Description = data.Description;
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
