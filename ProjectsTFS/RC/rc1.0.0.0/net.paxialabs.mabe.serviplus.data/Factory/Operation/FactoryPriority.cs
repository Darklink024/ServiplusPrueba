using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
   internal class FactoryPriority : BaseFactory<FactoryPriority, EntityPriority, Priority>
    {
        public override EntityPriority _GetEntity(Priority entidad)
        {
            return new EntityPriority()
            {
             PK_PriorityID = entidad.PK_PriorityID,
             FK_ScheduleID = entidad.FK_ScheduleID,
             Priority1 =entidad.Priority1,
             Description = entidad.Description,
             Status = entidad.Status,
             CreateDate = entidad.CreateDate,
             ModifyDate = entidad.ModifyDate,
             //Schedule = entidad.Schedule
            };
        }
    }
}
