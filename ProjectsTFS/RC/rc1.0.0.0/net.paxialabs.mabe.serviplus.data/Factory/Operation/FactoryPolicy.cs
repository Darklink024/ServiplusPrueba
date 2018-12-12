using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    //class FactoryPolicy
    //{
    //}
    internal class FactoryPolicy : BaseFactory<FactoryPolicy, EntityPolicy, Policy>
    {
        public override EntityPolicy _GetEntity(Policy entidad)
        {
            return new EntityPolicy()
            {
                PK_PolicyID=entidad.PK_PolicyID,
                FK_OrderID = entidad.FK_OrderID,
                FK_ClientID = entidad.FK_ClientID,
                FK_EmployeeID = entidad.FK_EmployeeID,
                FK_PaymentID = entidad.FK_PaymentID,
                FK_Invoice_ID = entidad.FK_Invoice_ID.HasValue ? entidad.FK_Invoice_ID:0,
                FK_QuotationID = entidad.FK_QuotationID,
                FK_ProductID = entidad.FK_ProductID,
                FK_ShopPlace= entidad.FK_ShopPlaceID.HasValue ? entidad.FK_ShopPlaceID.Value:0,
                IDPolicy = entidad.IDPolicy,
                PolicyPrice = entidad.PolicyPrice,
                Coin = entidad.Coin,
                SalesOrg = entidad.SalesOrg,
                ccGroup = entidad.ccGroup,
                PriceList= entidad.PriceList,
                MaterialGroup4=entidad.MaterialGroup4,
                SerialNumber= entidad.SerialNumber,
                GuarantyEnd = entidad.GuarantyEnd,
                GuarantyStart = entidad.GuarantyStart,
                PolicyDate = entidad.PolicyDate,
                Status=entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate,
               

            };
        }

    }
}
