using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public class FacadeLogMobile
    {
        public static string Insert(ModelViewLog model)
        {
            return new BusinessLogMobile().Insert(model);
        }

    }
}
