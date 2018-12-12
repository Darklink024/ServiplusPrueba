using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.security;

namespace net.paxialabs.mabe.serviplus.data
{
    public abstract class BaseRepository
    {
        protected db_PaxiaLabs_ServiplusEntities DataContext;
        public BaseRepository()
        {
            Z.EntityFramework.Extensions.LicenseManager.AddLicense(GlobalConfiguration.GetZ_EntityFramework_Extensions_LicenseName(), GlobalConfiguration.GetZ_EntityFramework_Extensions_LicenseKey());
            DataContext = new db_PaxiaLabs_ServiplusEntities();
        }
    }
}
