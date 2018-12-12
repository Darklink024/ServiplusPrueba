using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityDevicePhone
    {
        //DevicePhoneID, Phone, IMEI, FCM, Status, CreateDate, ModifyDate
        public int DevicePhoneID { get; set; }
        public string Phone { get; set; }
        public string IMEI { get; set; }
        public string FCM { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
