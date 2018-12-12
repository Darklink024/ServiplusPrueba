using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryPermission : BaseFactory<FactoryPermission, EntityPermission, Permission>
    {
        public override EntityPermission _GetEntity(Permission entidad)
        {
            return new EntityPermission()
            {
                ModuleID = entidad.ModuleID,
                ProfileID = entidad.ProfileID,
                Access = entidad.Access,
                Read = entidad.Read,
                Add = entidad.Add,
                Delete = entidad.Delete,
                Export = entidad.Export,
                Update = entidad.Update
            };
        }
    }
}
