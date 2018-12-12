using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessPayment
    {
        public ModelViewPayment Get(int ID)
        {
            var data = new RepositoryPayment().Get(ID);
            return new ModelViewPayment() {
                AuthorizationPayment = data.AuthorizationPayment,
                DatePayment = data.DatePayment.HasValue ? data.DatePayment.Value.ToString("dd/MM/yyyy hh:mm:ss") :  "",
                MountPayment = data.MountPayment.HasValue ? data.MountPayment.Value : 0,
                TypePaymentID = data.TypePaymentID,
                OrderID = new RepositoryOrder().Get(data.FK_OrderID).OrderID               
            };
        }

        public ModelViewPayment GetByOrderID(int OrderID)
        {
            var data = new RepositoryPayment().GetByOrderID(OrderID);
            if (data != null)
                return new ModelViewPayment()
                {
                    AuthorizationPayment = data.AuthorizationPayment,
                    DatePayment = data.DatePayment.HasValue ? data.DatePayment.Value.ToString("dd/MM/yyyy hh:mm:ss") : "",
                    MountPayment = data.MountPayment.HasValue ? data.MountPayment.Value : 0,
                    TypePaymentID = data.TypePaymentID,
                    OrderID = new RepositoryOrder().Get(data.FK_OrderID).OrderID
                };
            else
                return new ModelViewPayment();
        }

        public ModelViewPayment GetPolicyPayment(int OrderID, string Folio)
        {
            var data = new RepositoryPayment().GetPolicyPayment(OrderID, Folio);

            if (data != null)
                return new ModelViewPayment()
                {
                     PK_PaymentID= data.PK_PaymentID,
                     OrderID=data.FK_OrderID.ToString(),
                     TypePaymentID=data.TypePaymentID,
                     AuthorizationPayment=data.AuthorizationPayment,
                     DatePayment=data.DatePayment.ToString(),
                     MountPayment=data.MountPayment.Value,
                     Folio= data.Folio,
                     EstimatedType=data.Fk_TypeQuotation.Value
                };
            else
                return new ModelViewPayment();

        }

        public ModelViewPayment GetPaymentByType(int OrderID, int TypeQuotation)
        {
            var data = new RepositoryPayment().GetPaymentByType(OrderID, TypeQuotation);

            if (data != null)
                return new ModelViewPayment()
                {
                    PK_PaymentID = data.PK_PaymentID,
                    OrderID = data.FK_OrderID.ToString(),
                    TypePaymentID = data.TypePaymentID,
                    AuthorizationPayment = data.AuthorizationPayment,
                    DatePayment = data.DatePayment.ToString(),
                    MountPayment = data.MountPayment.Value,
                    Folio = data.Folio,
                    EstimatedType = data.Fk_TypeQuotation.Value
                };
            else
                return new ModelViewPayment();

        }

        public List<EntityPayment> GetAll()
        {
            return new RepositoryPayment().GetAll();
        }

        public void Insert(EntityPayment Pago)
        {
            new RepositoryPayment().Insert(Pago);
        }
        public void Update(EntityPayment Pago)
        {
            new RepositoryPayment().Update(Pago);
        }

    }
}
