using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System.Collections.Generic;


namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadePermission
    {
        public static List<ModelViewPermission> GetAll(int ProfileID)
        {
            return new BusinessPermission().GetAll(ProfileID);
        }

        public static ModelViewPermission Insert(ModelViewPermission model)
        {
            return new BusinessPermission().Insert(model);
        }

        public static ModelViewPermission Update(ModelViewPermission model)
        {
            return new BusinessPermission().Update(model);
        }
    }
}
