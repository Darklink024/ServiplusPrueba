using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryAudit : BaseFactory<FactoryAudit, EntityAudit, Audit>
    {
        public override EntityAudit _GetEntity(Audit entidad)
        {
            return new EntityAudit()
            {
                AuditID = entidad.AuditID,
                Action = entidad.Action,
                DateAudit = entidad.DateAudit,
                ModuleID = entidad.ModuleID,
                UserID = entidad.UserID
            };
        }
    }
}
