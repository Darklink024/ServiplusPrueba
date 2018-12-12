using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Operation
{
    public class FacadeStates
    {
        public static List<ModelViewStates> GetListStates(ModelViewUserG objCred)
        {
            return new BusinessStates().GetListStates(objCred);
        }
    }
}
