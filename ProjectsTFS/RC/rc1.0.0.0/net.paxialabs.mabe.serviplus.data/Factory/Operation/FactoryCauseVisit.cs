using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryCauseVisit : BaseFactory<FactoryCauseVisit, EntityCauseVisit, CauseVisit>
    {
         public override EntityCauseVisit _GetEntity(CauseVisit entidad)
        {
            return new EntityCauseVisit()
            {
                PK_CauseVisitID = entidad.PK_CauseVisitID,
                FK_StatusVisitID = entidad.FK_StatusVisitID,
                CauseVisit1 = entidad.CauseVisit1,
                Moment = entidad.Moment,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
