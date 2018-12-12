using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactorySchedule : BaseFactory<FactorySchedule, EntitySchedule, Schedule>
    {
        public override EntitySchedule _GetEntity(Schedule entidad)
        {
            return new EntitySchedule()
            {
                PK_ScheduleID = entidad.PK_ScheduleID,
                ScheduleStart = entidad.ScheduleStart,
                ScheduleEnd = entidad.ScheduleEnd,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
