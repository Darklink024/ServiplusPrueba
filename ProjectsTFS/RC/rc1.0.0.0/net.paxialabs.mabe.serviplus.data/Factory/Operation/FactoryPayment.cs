using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Operation
{
    internal class FactoryPayment : BaseFactory<FactoryPayment, EntityPayment, Payment>
    {
        public override EntityPayment _GetEntity(Payment entidad)
        {
            return new EntityPayment()
            {
               PK_PaymentID = entidad.PK_PaymentID,
               FK_OrderID = entidad.FK_OrderID,
               TypePaymentID = entidad.TypePaymentID,
               AuthorizationPayment = entidad.AuthorizationPayment,
               DatePayment = entidad.DatePayment,
               MountPayment = entidad.MountPayment,
               Status = entidad.Status,
               CreateDate = entidad.CreateDate,
               ModifyDate = entidad.ModifyDate,
               Folio=entidad.Folio,
               Fk_TypeQuotation= entidad.Fk_TypeQuotation
               
            };
        }
    }
}
