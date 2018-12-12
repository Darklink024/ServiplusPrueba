using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadeAudit
    {
        public static List<ModelViewAudit> GetAll(int ProfileID)
        {
            return new BusinessAudit().GetAll(ProfileID);
        }

        public static ModelViewAudit Insert(ModelViewAudit model)
        {
            return new BusinessAudit().Insert(model);
        }

        public static ModelViewAudit Update(ModelViewAudit model)
        {           
            return new BusinessAudit().Update(model);
        }
    }
}
