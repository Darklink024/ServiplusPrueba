using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessCauseVisit
    {
        public List<EntityCauseVisit> GetAll()
        {
            return new RepositoryCauseVisit().GetAll().Select(p => new EntityCauseVisit()
            {
                PK_CauseVisitID = p.PK_CauseVisitID,
                FK_StatusVisitID = p.FK_StatusVisitID,
                CauseVisit1 = p.CauseVisit1,
                Moment = p.Moment,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityCauseVisit>();
        }
    }
}
