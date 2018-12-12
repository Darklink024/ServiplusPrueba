using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryDevicePhone : BaseFactory<FactoryDevicePhone, EntityDevicePhone, DevicePhone>
    {
        public override EntityDevicePhone _GetEntity(DevicePhone entidad)
        {
            return new EntityDevicePhone()
            {
                DevicePhoneID = entidad.DevicePhoneID,
                IMEI = entidad.IMEI,
                Phone = entidad.Phone,
                FCM = entidad.FCM,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
