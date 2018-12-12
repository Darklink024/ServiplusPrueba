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
   public class RepositorySchedule : BaseRepository, IRepositoryGET<EntitySchedule>, IRepositorySET<EntitySchedule>
    {
        public EntitySchedule Get(int Id)
        {
            var data = base.DataContext.Schedule.Where(p => p.PK_ScheduleID == Id);
            if (data.Count() == 1)
                return FactorySchedule.Get(data.Single());
            else
                return null;
        }

        public List<EntitySchedule> GetActives()
        {
            return FactorySchedule.GetList(base.DataContext.Schedule.Where(p => p.Status == true).ToList());
        }

        public List<EntitySchedule> GetAll()
        {
            return FactorySchedule.GetList(base.DataContext.Schedule.ToList());
        }

        public EntitySchedule Insert(EntitySchedule data)
        {
            try
            {
                Schedule dataNew = new Schedule()
                {
                    PK_ScheduleID = data.PK_ScheduleID,
                    ScheduleStart = data.ScheduleStart,
                    ScheduleEnd = data.ScheduleEnd,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
                base.DataContext.Schedule.Add(dataNew);
                base.DataContext.SaveChanges();

                data.PK_ScheduleID = dataNew.PK_ScheduleID;

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

        public EntitySchedule Update(EntitySchedule data)
        {
            try
            {
                var dataUpdate = base.DataContext.Schedule.Where(p => p.PK_ScheduleID == data.PK_ScheduleID).SingleOrDefault();

                if (dataUpdate != null)
                {
                    //dataUpdate.PK_ScheduleID = data.PK_ScheduleID;
                    dataUpdate.ScheduleStart = data.ScheduleStart;
                    dataUpdate.ScheduleEnd = data.ScheduleEnd;
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
