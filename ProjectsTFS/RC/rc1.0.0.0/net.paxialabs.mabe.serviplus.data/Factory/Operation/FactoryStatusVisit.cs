using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryStatusVisit : BaseFactory<FactoryStatusVisit, EntityStatusVisit, StatusVisit>
    {
        public override EntityStatusVisit _GetEntity(StatusVisit entidad)
        {
            return new EntityStatusVisit()
            {
                PK_StatusVisitID = entidad.PK_StatusVisitID,
                StatusVisit1 = entidad.StatusVisit1,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
