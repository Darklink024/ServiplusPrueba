using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryRefSell : BaseFactory<FactoryRefSell, EntityRefSell, RefSell>
    {
        public override EntityRefSell _GetEntity(RefSell entidad)
        {
            return new EntityRefSell()
            {
                PK_RefSellID = entidad.PK_RefSellID,
                FK_OrderID = entidad.FK_OrderID,
                FK_ClientID = entidad.FK_ClientID,
                FK_EmployeeID = entidad.FK_EmployeeID,
                FK_PaymentID = entidad.FK_PaymentID,
                FK_Invoice_ID = entidad.FK_Invoice_ID,
                FK_QuotationID = entidad.FK_QuotationID,
                FK_ProductID = entidad.FK_ProductID,
                FK_ShopPlace=entidad.FK_ShopPlace.Value,
                IDRefSell = entidad.IDRefSell,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,


            };
        }
    }
}
