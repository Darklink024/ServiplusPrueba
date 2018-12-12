using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityDevicePhoneUser
    {
        //FK_DevicePhoneID, FK_UserID, Status, CreateDate, ModifyDate
        public int DevicePhoneID { get; set; }
        public int UserID { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
