using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryOrderEvidence : BaseFactory<FactoryOrderEvidence, EntityOrderEvidence, Evidence>
    {
        public override EntityOrderEvidence _GetEntity(Evidence entidad)
        {
            return new EntityOrderEvidence() {
                EvidenceID = entidad.PK_EvidenceID,
                OrderID = entidad.FK_OrderID,
                MonitorOrdersID = entidad.FK_MonitorOrdersID,
                TypeEvidence = entidad.TypeEvidence,
                URLEvidence = entidad.URLEvidence,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
