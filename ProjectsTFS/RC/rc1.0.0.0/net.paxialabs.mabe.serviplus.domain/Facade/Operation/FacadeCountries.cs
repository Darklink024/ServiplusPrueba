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
    public class FacadeCountries
    {
        public static List<ModelViewCountries> GetListCountries(ModelViewUserG objCred)
        {
            return new BusinessCountries().GetListCountries(objCred);
        }
    }
}
