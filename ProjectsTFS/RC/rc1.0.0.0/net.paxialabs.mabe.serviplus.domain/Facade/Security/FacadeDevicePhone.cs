using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Facade.Security
{
    public static class FacadeDevicePhone
    {
        public static ModelViewDevicePhone Get(int ID)
        {
            return new BusinessDevicePhone().Get(ID);
        }

        public static List<ModelViewDevicePhone> GetAll()
        {
            return new BusinessDevicePhone().GetAll();
        }

        public static List<ModelViewDevicePhone> GetActives()
        {
            return new BusinessDevicePhone().GetActives();
        }

        public static ModelViewDevicePhone Insert(ModelViewDevicePhone model)
        {
            return new BusinessDevicePhone().Insert(model);
        }

        public static ModelViewDevicePhone Update(ModelViewDevicePhone model)
        {
            return new BusinessDevicePhone().Update(model);
        }

    }
}
