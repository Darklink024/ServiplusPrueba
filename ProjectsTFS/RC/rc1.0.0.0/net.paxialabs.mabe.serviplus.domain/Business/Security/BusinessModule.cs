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
    internal class BusinessModule
    {
        public List<ModelViewModule> GetAll()
        {
            return new RepositoryModule().GetAll().Select(p => new ModelViewModule()
            {
                ModuleID = p.ModuleID,
                Module = p.Module,
                Description = p.Description,
                Status = p.Status,
                URL = p.URL,
                Section = p.Section
            }).ToList<ModelViewModule>();
        }

        public List<ModelViewModule> GetActives()
        {
            return new RepositoryModule().GetActives().Select(p => new ModelViewModule()
            {
                ModuleID = p.ModuleID,
                Module = p.Module,
                Description = p.Description,
                Status = p.Status,
                URL = p.URL,
                Section = p.Section
            }).ToList<ModelViewModule>();
        }

        public ModelViewModule Insert(ModelViewModule model)
        {
            model.Status = true;

            EntityModule data = new EntityModule()
            {
                ModuleID = model.ModuleID,
                Module = model.Module,
                Description = model.Description,
                Status = model.Status,
                URL = model.URL,
                Section = model.Section
            };

            data = new RepositoryModule().Insert(data);
            model.ModuleID = data.ModuleID;

            new BusinessPermission().Set(null, model.ModuleID);

            return model;
        }

        public ModelViewModule Update(ModelViewModule model)
        {
            EntityModule data = new EntityModule()
            {
                ModuleID = model.ModuleID,
                Module = model.Module,
                Description = model.Description,
                Status = model.Status,
                URL = model.URL,
                Section = model.Section
            };

            data = new RepositoryModule().Update(data);
            model.ModuleID = data.ModuleID;

            return model;
        }

        public void SetStatus(List<int> IDs)
        {
            foreach (var item in IDs)
            {
                var data = new RepositoryModule().Get(item);
                data.Status = !data.Status;
                new RepositoryModule().Update(data);
            }
        }
    }
}
