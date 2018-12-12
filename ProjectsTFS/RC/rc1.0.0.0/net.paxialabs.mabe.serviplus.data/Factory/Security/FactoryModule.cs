using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryModule : BaseFactory<FactoryModule, EntityModule, Module>
    {
        public override EntityModule _GetEntity(Module entidad)
        {
            return new EntityModule() {
                ModuleID = entidad.ModuleID,
                Module = entidad.Module1,
                Description = entidad.Description,
                Status = entidad.Status,
                URL = entidad.URL,
                Section = entidad.Section
            };
        }
    }
}
