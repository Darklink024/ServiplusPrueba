using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    internal class BusinessDevicePhoneUser
    {
        public ModelViewDevicePhoneUser Get(int DevicePhoneID, int UserID)
        {
            var data = new RepositoryDevicePhoneUser().Get(DevicePhoneID, UserID);
            if (data != null)
            {
                return new ModelViewDevicePhoneUser() {
                    DevicePhoneID = data.DevicePhoneID,
                    UserID = data.UserID,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
            }
            return null;
        }

        public List<ModelViewDevicePhoneUser> GetAll()
        {
            return new RepositoryDevicePhoneUser().GetAll().Select(p => new ModelViewDevicePhoneUser()
            {
                DevicePhoneID = p.DevicePhoneID,
                UserID = p.UserID,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public List<ModelViewDevicePhoneUser> GetActives()
        {
            return new RepositoryDevicePhoneUser().GetActives().Select(p => new ModelViewDevicePhoneUser()
            {
                DevicePhoneID = p.DevicePhoneID,
                UserID = p.UserID,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public List<ModelViewDevicePhoneUser> GetByUserIDs(List<int> UserIDs)
        {
            return new RepositoryDevicePhoneUser().GetByUserIDs(UserIDs).Select(p => new ModelViewDevicePhoneUser()
            {
                DevicePhoneID = p.DevicePhoneID,
                UserID = p.UserID,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
                FCM = new BusinessDevicePhone().Get(p.DevicePhoneID).FCM
            }).ToList();
        }

        public List<ModelViewDevicePhoneUser> GetByUser(int UserID)
        {
            return new RepositoryDevicePhoneUser().GetByUser(UserID).Select(p => new ModelViewDevicePhoneUser()
            {
                DevicePhoneID = p.DevicePhoneID,
                UserID = p.UserID,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public List<ModelViewDevicePhoneUser> GetByDevicePhone(int DevicePhoneID)
        {
            return new RepositoryDevicePhoneUser().GetByDevicePhone(DevicePhoneID).Select(p => new ModelViewDevicePhoneUser()
            {
                DevicePhoneID = p.DevicePhoneID,
                UserID = p.UserID,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public ModelViewDevicePhoneUser Insert(ModelViewDevicePhoneUser model)
        {
            return (ModelViewDevicePhoneUser)new RepositoryDevicePhoneUser().Insert(model);
        }

        public ModelViewDevicePhoneUser Update(ModelViewDevicePhoneUser model)
        {
            return (ModelViewDevicePhoneUser)new RepositoryDevicePhoneUser().Update(model);
        }
    }
}
