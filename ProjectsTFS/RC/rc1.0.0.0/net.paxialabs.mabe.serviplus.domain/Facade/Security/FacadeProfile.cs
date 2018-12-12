using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadeProfile
    {
        public static List<ModelViewProfile> GetAll()
        {
            return new BusinessProfile().GetAll();
        }

        public static List<ModelViewProfile> GetActives()
        {
            return new BusinessProfile().GetActives();
        }

        public static ModelViewProfile Insert(ModelViewProfile model)
        {
            return new BusinessProfile().Insert(model);
        }

        public static ModelViewProfile Update(ModelViewProfile model)
        {
            return new BusinessProfile().Update(model);
        }

        public static void SetStatus(List<int> IDs)
        {
            new BusinessProfile().SetStatus(IDs);
        }
    }
}
