using net.paxialabs.mabe.serviplus.data.Repository.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    internal class BusinessDevicePhone
    {
        public ModelViewDevicePhone Get(int ID)
        {
            var data = new RepositoryDevicePhone().Get(ID);
            return new ModelViewDevicePhone() {
                DevicePhoneID = data.DevicePhoneID,
                Phone = data.Phone,
                IMEI = data.IMEI,
                FCM = data.FCM,
                Status = data.Status,
                CreateDate = data.CreateDate,
                ModifyDate = data.ModifyDate
            };
        }

        public ModelViewDevicePhone GetByIMEI(string IMEI)
        {
            var data = new RepositoryDevicePhone().GetByIMEI(IMEI);
            if (data != null)
            {
                return new ModelViewDevicePhone()
                {
                    DevicePhoneID = data.DevicePhoneID,
                    Phone = data.Phone,
                    IMEI = data.IMEI,
                    FCM = data.FCM,
                    Status = data.Status,
                    CreateDate = data.CreateDate,
                    ModifyDate = data.ModifyDate
                };
            }

            return null;
        }

        public List<ModelViewDevicePhone> GetAll()
        {
            return new RepositoryDevicePhone().GetAll().Select(p => new ModelViewDevicePhone() {
                DevicePhoneID = p.DevicePhoneID,
                Phone = p.Phone,
                IMEI = p.IMEI,
                FCM = p.FCM,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public List<ModelViewDevicePhone> GetActives()
        {
            return new RepositoryDevicePhone().GetActives().Select(p => new ModelViewDevicePhone()
            {
                DevicePhoneID = p.DevicePhoneID,
                Phone = p.Phone,
                IMEI = p.IMEI,
                FCM = p.FCM,
                Status = p.Status,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList();
        }

        public ModelViewDevicePhone Insert(ModelViewDevicePhone model)
        {
            return (ModelViewDevicePhone) new RepositoryDevicePhone().Insert(model);
        }

        public ModelViewDevicePhone Update(ModelViewDevicePhone model)
        {
            return (ModelViewDevicePhone)new RepositoryDevicePhone().Update(model);
        }

        public ModelViewDevicePhone Register(ModelViewDevicePhone model)
        {
            if (new RepositoryDevicePhone().GetByIMEI(model.IMEI) == null)
            {
                var entity = new RepositoryDevicePhone().Insert(model);
                model.DevicePhoneID = entity.DevicePhoneID;
            }
            return model;
        }
    }
}
