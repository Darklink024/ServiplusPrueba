using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityHistory
    {
        public int PK_HistoryID { get; set; }
        public Nullable<int> FK_InstalledBaseID { get; set; }
        public Nullable<int> FK_ClientID { get; set; }
        public Nullable<int> FK_OrderID { get; set; }
        public string OrderID { get; set; }
        public string OrderStatus { get; set; }
        public string ItemStatus { get; set; }
        public string Guaranty { get; set; }
        public System.DateTime ShopDate { get; set; }
        public System.DateTime CloseDate { get; set; }
        public string FailureID1 { get; set; }
        public string Failure1 { get; set; }
        public string FailureID2 { get; set; }
        public string Failure2 { get; set; }
        public string FailureID3 { get; set; }
        public string Failure3 { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
