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
    public class FacadeStatusCauseVisit
    {
        public static List<EntityStatusVisit> GetAll()
        {
            return new BusinessStatusCauseVisit().GetAll();
        }

        public static List<ModelViewCauseVisit> GetListStatusVisit(ModelViewUserG objCred)
        {
            return new BusinessStatusCauseVisit().GetListCauseOrder(objCred);
        }
    }
}
