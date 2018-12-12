using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public static class FacadePayment
    {
        public static ModelViewPayment Get(int PaymentID)
        {
            return new BusinessPayment().Get(PaymentID);
        }

        public static ModelViewPayment GetByOrderID(int OrderID)
        {
            return new BusinessPayment().GetByOrderID(OrderID);
        }
        public static ModelViewPayment GetPaymetByType(int OrderID)
        {
            int TypeQuotation = 1;
            return new BusinessPayment().GetPaymentByType(OrderID, TypeQuotation);
        }
        

        public static ModelViewResultREST SetPayment(ModelViewPayment model)
        {
            try
            {
                var dataUsuario = new BusinessUsers().GetUserByToken(model.TokenUser);
                if (model.TokenApp != GlobalConfiguration.TokenWEB)
                    if (model.TokenApp != GlobalConfiguration.TokenMobile)
                        throw new Exception("TokenInvalid");
                if (dataUsuario == null) throw new Exception("UserPasswordInvalid");
                var orden = new BusinessOrder().GetByOrderID(model.OrderID);
                var payment = new BusinessPayment().GetPolicyPayment(orden.PK_OrderID, model.Folio);

                if (payment.OrderID== null) { 

                    new BusinessPayment().Insert(new entities.Entity.Operation.EntityPayment()
                    {
                        PK_PaymentID = 0,
                        FK_OrderID = new BusinessOrder().GetByOrderID(model.OrderID).PK_OrderID,
                        AuthorizationPayment = model.AuthorizationPayment,
                        DatePayment = Convert.ToDateTime(model.DatePayment),
                        MountPayment = model.MountPayment,
                        TypePaymentID = model.TypePaymentID,
                        Status = true,
                        CreateDate = DateTime.UtcNow,
                        ModifyDate = DateTime.UtcNow,
                        Folio=model.Folio,
                        Fk_TypeQuotation = model.EstimatedType
                       });
                 }
                else
                {
                    new BusinessPayment().Update(new entities.Entity.Operation.EntityPayment()
                    {
                        PK_PaymentID = payment.PK_PaymentID,
                        FK_OrderID = new BusinessOrder().GetByOrderID(model.OrderID).PK_OrderID,
                        AuthorizationPayment = model.AuthorizationPayment,
                        DatePayment = Convert.ToDateTime(model.DatePayment),
                        MountPayment = model.MountPayment,
                        TypePaymentID = model.TypePaymentID,
                        Status = true,
                        CreateDate = DateTime.UtcNow,
                        ModifyDate = DateTime.UtcNow,
                        Folio=model.Folio,
                        Fk_TypeQuotation=model.EstimatedType
                      
                       
                    });

                }
                return new ModelViewResultREST() { Result = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
