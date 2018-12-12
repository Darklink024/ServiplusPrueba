using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data.Factory.Security
{
    internal class FactoryDevicePhoneUser : BaseFactory<FactoryDevicePhoneUser, EntityDevicePhoneUser, DevicePhoneUser>
    {
        public override EntityDevicePhoneUser _GetEntity(DevicePhoneUser entidad)
        {
            return new EntityDevicePhoneUser() {
                DevicePhoneID = entidad.FK_DevicePhoneID,
                UserID = entidad.FK_UserID,
                Status = entidad.Status,
                CreateDate = entidad.CreateDate,
                ModifyDate = entidad.ModifyDate
            };
        }
    }
}
