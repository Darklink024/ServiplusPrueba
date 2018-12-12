using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeHistory
    {
        public static void Insert(int FK_InstalledBaseID, int FK_ClientID, int FK_OrderID, string OrderID, string FechaCompra, string TipoServicio, string SintomaFalla)
        {
            new BusinessHistory().Insert(FK_InstalledBaseID, FK_ClientID, FK_OrderID, OrderID, FechaCompra, TipoServicio, SintomaFalla);
        }

        public static void Insert(EntityHistory data)
        {
            new BusinessHistory().Insert(data);
        }

        public static void Update(EntityHistory visitas, string FechaCompra, string TipoServicio, string SintomaFalla)
        {
            new BusinessHistory().Update(visitas, FechaCompra, TipoServicio, SintomaFalla);
        }

        public static void Update(EntityHistory data)
        {
            new BusinessHistory().Update(data);
        }

        public static List<EntityHistory> GetAll()
        {
            return new BusinessHistory().GetAll();
        }
        public static EntityHistory GetByID(int ID)
        {
            return new BusinessHistory().GetByID(ID);
        }

        public static List<EntityHistory> GetByOrderID(string OrderID)
        {
            return new BusinessHistory().GetByOrderID(OrderID);
        }

        public static List<EntityHistory> GetByOrderID(int OrderID)
        {
            return new BusinessHistory().GetByOrderID(OrderID);
        }

        public static List<HistoricProduct> GetHP(int OrderID)
        {
            return new BusinessHistory().GetHP(OrderID);
        }
    }
}
