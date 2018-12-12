using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessRefsell
    {

        public EntityRefSell GetByOrder(int OrderID)
        {
            return new RepositoryRefSell().GetByOrder(OrderID);

        }
        public EntityRefSell Update(EntityRefSell data)
        {
            return new RepositoryRefSell().Update(data);
        }
        public List<EntityRefSell> GetByEmpoyeeDate(int UserID, DateTime date)
        {
            return new RepositoryRefSell().GetByEmpoyeeDate(UserID, date);
        }
        public EntityRefSell Insert(EntityRefSell Refaccion)
        {
            var objRepository = new RepositoryRefSell();
            EntityRefSell data = new EntityRefSell()
            {
               PK_RefSellID = 0,
                FK_OrderID = Refaccion.FK_OrderID,
                FK_ClientID = Refaccion.FK_ClientID,
                FK_EmployeeID = Refaccion.FK_EmployeeID,
                FK_QuotationID = Refaccion.FK_QuotationID,
                FK_ProductID = Refaccion.FK_ProductID,
                FK_ShopPlace=Refaccion.FK_ShopPlace,
                IDRefSell = Refaccion.IDRefSell,
             
                Status = true,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyDate = DateTime.UtcNow.ToLocalTime()

            };
            data = objRepository.Insert(data);
            return data;
        }
        public EntityRefSell GetByFolio(int OrderID, string Folio)
        {
            var data = new RepositoryRefSell().GetRefFolio(OrderID, Folio);

            if (data != null)
                return new EntityRefSell()
                {
                    PK_RefSellID = data.PK_RefSellID,
                    FK_OrderID = data.FK_OrderID,
                    FK_ClientID = data.FK_ClientID,
                    FK_EmployeeID = data.FK_EmployeeID,
                    FK_PaymentID = data.FK_PaymentID,
                    FK_Invoice_ID = data.FK_Invoice_ID,
                    FK_QuotationID = data.FK_QuotationID,
                    FK_ProductID = data.FK_ProductID,
                    FK_ShopPlace=data.FK_ShopPlace,
                    IDRefSell = data.IDRefSell,
                    OrdenVenta = data.OrdenVenta,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
            else
                return new EntityRefSell();
        }
    }
}
