using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryVisit : BaseFactory<FactoryVisit, EntityVisit, MonitorOrders>
    {
        public override EntityVisit _GetEntity(MonitorOrders entidad)
        {
            return new EntityVisit()
            {
                PK_VisitID = entidad.PK_MonitorOrdersID,
                FK_OrderID = entidad.FK_OrderID,
                FK_StatusVisitID = entidad.FK_StatusVisitID,
                FK_StatusOrderID = entidad.FK_StatusOrderID,
                FK_CauseOrderID = entidad.FK_CauseOrderID,
                SequenceVisit = entidad.SequenceVisit,
                StartVisitDate = entidad.StartVisitDate,
                EndVisitDate = entidad.EndVisitDate,
                StartOrderDate = entidad.StartOrderDate,
                EndOrderDate = entidad.EndOrderDate,
                StartServiceDate = entidad.StartServiceDate,
                EndServiceDate = entidad.EndServiceDate,
                LatitudeAddress = entidad.LatitudeAddress,
                LogitudeAddress = entidad.LogitudeAddress,
                LatitudeStartVisit = entidad.LatitudeStartVisit,
                LogitudeStartVisit = entidad.LogitudeStartVisit,
                LatitudeEndVisit = entidad.LatitudeEndVisit,
                LogitudeEndVisit = entidad.LogitudeEndVisit,
                LatitudeStartOrder = entidad.LatitudeStartOrder,
                LogitudeStartOrder = entidad.LogitudeStartOrder,
                LatitudeEndOrder = entidad.LatitudeEndOrder,
                LogitudeEndOrder = entidad.LogitudeEndOrder,
                DurationVisit = entidad.DurationVisit,
                DurationOrder = entidad.DurationOrder,
                DurationExecute = entidad.DurationExecute,
                DurationTransport = entidad.DurationTransport,
                NoteVisit = entidad.NoteVisit,
                NoteOrder = entidad.NoteOrder,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
                ExtraKilometrer = entidad.ExtraKilometrer
            };
        }
    }
}
