using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadeDevicePhoneUser
    {
        public static ModelViewDevicePhoneUser Get(int DevicePhoneID, int UserID)
        {
            return new BusinessDevicePhoneUser().Get(DevicePhoneID, UserID);
        }

        public static List<ModelViewDevicePhoneUser> GetAll()
        {
            return new BusinessDevicePhoneUser().GetAll();
        }

        public static List<ModelViewDevicePhoneUser> GetActives()
        {
            return new BusinessDevicePhoneUser().GetActives();
        }

        public static List<ModelViewDevicePhoneUser> GetByUser(int UserID)
        {
            return new BusinessDevicePhoneUser().GetByUser(UserID);
        }

        public static List<ModelViewDevicePhoneUser> GetByDevicePhone(int DevicePhoneID)
        {
            return new BusinessDevicePhoneUser().GetByDevicePhone(DevicePhoneID);           
        }

        public static ModelViewDevicePhoneUser Insert(ModelViewDevicePhoneUser model)
        {
            return new BusinessDevicePhoneUser().Insert(model);
        }

        public static ModelViewDevicePhoneUser Update(ModelViewDevicePhoneUser model)
        {
            return new BusinessDevicePhoneUser().Update(model);
        }
    }
}
