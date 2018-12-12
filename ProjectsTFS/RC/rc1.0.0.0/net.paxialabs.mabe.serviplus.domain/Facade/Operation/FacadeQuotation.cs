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
    public class FacadeQuotation
    {
        public static List<ModelViewFolios> GetListFolios(ModelViewUserG objCred)
        {
            return new BusinessQuotation().GetLastFolio(objCred);
        }
    }
}
