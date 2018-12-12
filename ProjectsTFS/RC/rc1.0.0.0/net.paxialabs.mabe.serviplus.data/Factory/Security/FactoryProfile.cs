using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryProfile : BaseFactory<FactoryProfile, EntityProfile, Profile>
    {
        public override EntityProfile _GetEntity(Profile entidad)
        {
            return new EntityProfile() {
                ProfileID = entidad.ProfileID,
                Profile = entidad.Profile1,
                Description = entidad.Description,
                Status = entidad.Status
            };
        }
    }
}
