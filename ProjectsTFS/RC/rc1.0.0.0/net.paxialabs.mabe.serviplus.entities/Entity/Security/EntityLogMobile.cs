using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityLogMobile
    {
        public int PK_LogMobileID { get; set; }
        public Nullable<int> FK_OrderID { get; set; }
        public Nullable<int> FK_UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string OrderID { get; set; }
        public string Module { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public string SignType { get; set; }
        public string Battery { get; set; }
        public string SignPercentage { get; set; }
        public string ConnectionType { get; set; }
        public string MobileModel { get; set; }
        public string MobileStorage { get; set; }
        public string version { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
