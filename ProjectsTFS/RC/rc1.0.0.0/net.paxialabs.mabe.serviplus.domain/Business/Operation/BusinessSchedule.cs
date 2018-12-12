using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessSchedule
    {

        public List<ModelViewPriority> GetListAll()
        {
            var dataschedule = GetAll(); 
            var NegocioPrioridad = new BusinessPriority();
            var dataPriority = NegocioPrioridad.GetAll();
            return (from c in dataPriority
                    join p in dataschedule on c.FK_ScheduleID equals p.PK_ScheduleID
                    select new ModelViewPriority()
                    {   PriorityID = c.PK_PriorityID, ScheduleID = c.FK_ScheduleID, Description = c.Description, Status = c.Status,
                        Priority = c.Priority1, ScheduleStart = p.ScheduleStart, ScheduleEnd = p.ScheduleEnd }).ToList(); ;
        }
        public List<EntitySchedule> GetAll()
        {
            return new RepositorySchedule().GetAll().Select(p => new EntitySchedule()
            {
                PK_ScheduleID = p.PK_ScheduleID,
                ScheduleStart = p.ScheduleStart,
                ScheduleEnd = p.ScheduleEnd,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
            }).ToList<EntitySchedule>();
        }

        public void SetStatus(List<int> IDs)
        {
            foreach (var item in IDs)
            {
                var data = new RepositorySchedule().Get(item);
                data.Status = !data.Status;
                data.ModifyDate = DateTime.UtcNow;
                new RepositorySchedule().Update(data);
            }
        }

        public ModelViewPriority Update(ModelViewPriority model)
        {
            var objRepository = new RepositorySchedule();

            var dataOld = new RepositorySchedule().Get(model.ScheduleID);

            EntitySchedule data = new EntitySchedule()
            {
                PK_ScheduleID = dataOld.PK_ScheduleID,
                ScheduleStart = model.ScheduleStart,
                ScheduleEnd = model.ScheduleEnd,
                Status = dataOld.Status,
                CreateDate = dataOld.CreateDate,
                ModifyDate = DateTime.UtcNow
 
            };

            data = objRepository.Update(data);

            return model;
        }
    }
}
