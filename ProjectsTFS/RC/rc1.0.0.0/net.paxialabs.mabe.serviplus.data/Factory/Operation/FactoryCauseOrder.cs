using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryCauseOrder : BaseFactory<FactoryCauseOrder, EntityCauseOrder, CauseOrder>
    {
        public override EntityCauseOrder _GetEntity(CauseOrder entidad)
        {
            return new EntityCauseOrder()
            {
             PK_CauseOrderID = entidad.PK_CauseOrderID,
             FK_StatusOrderID = entidad.FK_StatusOrderID,
             CauseOrder1 = entidad.CauseOrder1,
             Moment = entidad.Moment,
             Status = entidad.Status,
             CreateDate = entidad.CreateDate,
             ModifyDate = entidad.ModifyDate
            };
        }
    }
}
