using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System.Collections.Generic;
using System.Globalization;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadeSecurity
    {
        public static ModelViewUser Authenticate(ModelViewLogin model)
        {
            return new BusinessSecurity().Authenticate(model);   
        }

        public static bool AccessValidation(string token, string URL)
        {
            return new BusinessPermission().Validate(token, URL);
        }

        public static bool AccessValidation(string token, string URL, AuditAction action)
        {
            return new BusinessPermission().Validate(token, URL, action);
        }

        public static void Recovery(ModelViewRecovery model)
        {
             new BusinessSecurity().Recovery(model);
        }

        public static void ChangePassword(ModelViewChangePassword model)
        {
            new BusinessSecurity().ChangePassword(model);
        }
        public static List<EntityLogMobile> GetBillings(string DateIn, string DateFn, string Messange)
        {
           return new BusinessSecurity().GEtBilling(DateIn,DateFn,Messange);
        }


    }
}
