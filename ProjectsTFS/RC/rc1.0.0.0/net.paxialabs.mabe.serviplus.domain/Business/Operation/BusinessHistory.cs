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
    internal class BusinessHistory
    {
        public void Insert(int FK_InstalledBaseID, int FK_ClientID, int FK_OrderID, string OrderID, string FechaCompra, string TipoServicio, string SintomaFalla)
        {
            DateTime compra = DateTime.Parse(FechaCompra);
            var objRepository = new RepositoryHistory();
            EntityHistory data = new EntityHistory()
            {
                PK_HistoryID = 0,
                FK_InstalledBaseID = FK_InstalledBaseID,
                FK_ClientID = FK_ClientID,
                FK_OrderID = FK_OrderID,
                OrderID = OrderID,
                OrderStatus = "",
                ItemStatus = "",
                Guaranty = TipoServicio,
                ShopDate = compra,
                CloseDate = DateTime.UtcNow,
                FailureID1 = "",
                Failure1 = SintomaFalla,
                FailureID2 = "",
                Failure2 = "",
                FailureID3 = "",
                Failure3 = "",
                Status = true,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);

        }

        public void Update(EntityHistory visitas, string FechaCompra, string TipoServicio, string SintomaFalla)
        {
            DateTime compra = DateTime.Parse(FechaCompra);
            var objRepository = new RepositoryHistory();
            EntityHistory data = new EntityHistory()
            {
                PK_HistoryID = visitas.PK_HistoryID,
                FK_InstalledBaseID = visitas.FK_InstalledBaseID,
                FK_ClientID = visitas.FK_ClientID,
                FK_OrderID = visitas.FK_OrderID,
                OrderID = visitas.OrderID,
                OrderStatus = visitas.OrderStatus,
                ItemStatus = visitas.ItemStatus,
                Guaranty = TipoServicio,
                ShopDate = compra,
                CloseDate = visitas.CloseDate,
                FailureID1 = visitas.FailureID1,
                Failure1 = SintomaFalla,
                FailureID2 = visitas.FailureID2,
                Failure2 = visitas.Failure2,
                FailureID3 = visitas.FailureID3,
                Failure3 = visitas.Failure3,
                Status = visitas.Status,
                CreateDate = visitas.CreateDate,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Update(data);

        }

        public void Update(EntityHistory data)
        {
            new RepositoryHistory().Update(data);
        }

        public void Insert(EntityHistory data)
        {
            new RepositoryHistory().Insert(data);
        }

        public List<EntityHistory> GetAll()
        {
            return new RepositoryHistory().GetAll().Select(p => new EntityHistory()
            {
                PK_HistoryID = p.PK_HistoryID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_OrderID = p.FK_OrderID,
                OrderID = p.OrderID,
                OrderStatus = p.OrderStatus,
                ItemStatus = p.ItemStatus,
                Guaranty = p.Guaranty,
                ShopDate = p.ShopDate,
                CloseDate = p.CloseDate,
                FailureID1 = p.FailureID1,
                Failure1 = p.Failure1,
                FailureID2 = p.FailureID2,
                Failure2 = p.Failure2,
                FailureID3 = p.FailureID3,
                Failure3 = p.Failure3,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate

            }).ToList();
        }

        public EntityHistory GetByID(int ID)
        {
            return new RepositoryHistory().Get(ID);
        }

        public List<EntityHistory> GetByOrderID(string OrderID)
        {
            return new RepositoryHistory().GetByOrderID(OrderID);
        }


        public List<EntityHistory> GetByOrderID(int OrderID)
        {
            return new RepositoryHistory().GetByOrderID(OrderID);
        }

        public List<HistoricProduct> GetHP(int OrderID)
        {
            return new RepositoryHistory().GetList(OrderID);
        }
    }
}
