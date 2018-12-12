using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryMonitorOrder : BaseFactory<FactoryMonitorOrder, EntityMonitorOrder, MonitorOrders>
    {
        public override EntityMonitorOrder _GetEntity(MonitorOrders entidad)
        {
            return new EntityMonitorOrder()
            {
                VisitID = entidad.PK_MonitorOrdersID,
                OrderID = entidad.FK_OrderID,
                StatusVisitID = entidad.FK_StatusVisitID,
                StatusOrderID = entidad.FK_StatusOrderID,
                CauseOrderID = entidad.FK_CauseOrderID,
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
                LatitudeEndOrder = entidad.LatitudeEndOrder,
                LogitudeStartOrder = entidad.LogitudeStartOrder,
                LogitudeEndOrder = entidad.LogitudeEndOrder,
                DurationVisit = entidad.DurationVisit != null ? entidad.DurationVisit.Value : entidad.DurationVisit,
                DurationExecute = entidad.DurationExecute != null ? entidad.DurationExecute.Value : entidad.DurationExecute,
                DurationTransport = entidad.DurationTransport != null ? entidad.DurationTransport.Value : entidad.DurationTransport,
                NoteVisit = entidad.NoteVisit,
                NoteOrder = entidad.NoteOrder,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
                ExtraKilometrer= entidad.ExtraKilometrer

            };
        }
    }
}
