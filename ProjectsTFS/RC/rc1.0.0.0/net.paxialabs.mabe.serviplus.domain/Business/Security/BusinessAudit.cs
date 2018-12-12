using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    internal class BusinessAudit
    {
        public List<ModelViewAudit> GetAll(int ProfileID)
        {
            return new RepositoryAudit().GetAll().Select(p => new ModelViewAudit()
            {
                ModuleID = p.ModuleID,
                Action = p.Action,
                AuditID = p.AuditID,
                DateAudit = p.DateAudit,
                UserID = p.UserID
            }).ToList<ModelViewAudit>();
        }

        public ModelViewAudit Insert(ModelViewAudit model)
        {
            model.DateAudit = DateTime.UtcNow;

            EntityAudit data = new EntityAudit()
            {
                ModuleID = model.ModuleID,
                Action = model.Action,
                AuditID = model.AuditID,
                DateAudit = model.DateAudit,
                UserID = model.UserID
            };

            data = new RepositoryAudit().Insert(data);

            return model;
        }

        public ModelViewAudit Update(ModelViewAudit model)
        {
            EntityAudit data = new EntityAudit()
            {
                ModuleID = model.ModuleID,
                Action = model.Action,
                AuditID = model.AuditID,
                DateAudit = model.DateAudit,
                UserID = model.UserID
            };

            data = new RepositoryAudit().Update(data);

            return model;
        }
    }
}
