using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
   public class FacadeCauseVisit
    {
        public static List<EntityCauseVisit> GetAll()
        {
            return new BusinessCauseVisit().GetAll();
        }
    }
}
