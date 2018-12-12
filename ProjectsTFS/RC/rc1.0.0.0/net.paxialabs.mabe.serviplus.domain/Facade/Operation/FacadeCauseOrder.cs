using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeCauseOrder
    {
        public static List<ModelViewCauseOrder> GetListCauseOrder(ModelViewUserG objCred)
        {
            return new BusinessCauseOrder().GetListCauseOrder(objCred);
        }
        public static List<EntityCauseOrder> GetAll()
        {
            return new BusinessCauseOrder().GetAll();
        }
    }
}
