using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryHistory : BaseFactory<FactoryHistory, EntityHistory, History>
    {
        public override EntityHistory _GetEntity(History entidad)
        {
            return new EntityHistory()
            {
             PK_HistoryID = entidad.PK_HistoryID,
             FK_InstalledBaseID = entidad.FK_InstalledBaseID,
             FK_ClientID = entidad.FK_ClientID,
             FK_OrderID = entidad.FK_OrderID,
             OrderID = entidad.OrderID,
             OrderStatus = entidad.OrderStatus,
             ItemStatus= entidad.ItemStatus,
             Guaranty = entidad.Guaranty,
             ShopDate= entidad.ShopDate,
             CloseDate = entidad.CloseDate,
             FailureID1 = entidad.FailureID1,
             Failure1 = entidad.Failure1,
             FailureID2 = entidad.FailureID2,
             Failure2 = entidad.Failure2,
             FailureID3 = entidad.FailureID3,
             Failure3 = entidad.Failure3,
             Status = entidad.Status,
             CreateDate = entidad.CreateDate,
             ModifyDate = entidad.ModifyDate
            };
        }
    }
}
