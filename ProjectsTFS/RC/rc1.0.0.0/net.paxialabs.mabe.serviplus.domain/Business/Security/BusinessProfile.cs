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
    internal class BusinessProfile
    {
        public ModelViewProfile Get(int ID)
        {
            var data = new RepositoryProfile().Get(ID);

            var result = new ModelViewProfile()
            {
                ProfileID = data.ProfileID,
                Profile = data.Profile,
                Description = data.Description,
                Status = data.Status
            };

            return result;
        }

        public List<ModelViewProfile> GetAll()
        {
            return new RepositoryProfile().GetAll().Select(p => new ModelViewProfile()
            {                
                ProfileID = p.ProfileID,
                Profile = p.Profile,
                Description = p.Description,                
                Status = p.Status                
            }).ToList<ModelViewProfile>();
        }

        public List<ModelViewProfile> GetActives()
        {
            return new RepositoryProfile().GetActives().Select(p => new ModelViewProfile()
            {
                ProfileID = p.ProfileID,
                Profile = p.Profile,
                Description = p.Description,
                Status = p.Status
            }).ToList<ModelViewProfile>();
        }

        public ModelViewProfile Insert(ModelViewProfile model)
        {
            var objRepository = new RepositoryProfile();
            model.Status = true;
            if (objRepository.GetAll().Where(p => p.Profile.ToUpper() == model.Profile.ToUpper()) != null) throw new Exception("Duplicate");

            EntityProfile data = new EntityProfile() {
                ProfileID = model.ProfileID,
                Profile = model.Profile,
                Description = model.Description,
                Status = model.Status
            };



            data = new RepositoryProfile().Insert(data);
            model.ProfileID = data.ProfileID;

            new BusinessPermission().Set(model.ProfileID, null);

            return model;
        }

        public ModelViewProfile Update(ModelViewProfile model)
        {
            EntityProfile data = new EntityProfile()
            {
                ProfileID = model.ProfileID,
                Profile = model.Profile,
                Description = model.Description,
                Status = model.Status
            };

            data = new RepositoryProfile().Update(data);
            model.ProfileID = data.ProfileID;

            return model;
        }

        public void SetStatus(List<int> IDs)
        {
            foreach (var item in IDs)
            {
                var data = Get(item);
                data.Status = !data.Status;
                Update(data);
            }
        }
    }
}
