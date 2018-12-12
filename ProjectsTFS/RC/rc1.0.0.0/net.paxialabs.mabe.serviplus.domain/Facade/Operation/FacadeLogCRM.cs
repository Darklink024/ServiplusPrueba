using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeLogCRM
    {
        public static EntityLogCRM GetByOrderID(int OrderID)
        {
            return new BusinessLogCRM().GetByOrderID(OrderID);
        }
    }
}
