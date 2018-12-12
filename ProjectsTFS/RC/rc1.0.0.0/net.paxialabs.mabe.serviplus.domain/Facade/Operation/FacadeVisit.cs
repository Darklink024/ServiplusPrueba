using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public static class FacadeVisit
    {
        public static void Insert(int FK_OrderID,int FK_StatusVisitID, int FK_StatusOrderID, double? LatitudeAddress, double? LogitudeAddress)
        {
            new BusinessVisit().Insert( FK_OrderID, FK_StatusVisitID, FK_StatusOrderID, LatitudeAddress, LogitudeAddress);
        }

        public static void Update(EntityVisit visit)
        {
            new BusinessVisit().Update(visit);
        }

        public static List<EntityVisit> GetAll()
        {
            return new BusinessVisit().GetAll();
        }

        public static EntityVisit GetByOrderID(int OrderID)
        {
            return new BusinessVisit().GetByOrderID(OrderID);
        }
    }
}
