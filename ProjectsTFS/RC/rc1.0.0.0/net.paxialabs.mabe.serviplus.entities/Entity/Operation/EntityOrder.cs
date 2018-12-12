using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityOrder
    {
        public int PK_OrderID { get; set; }
        public int FK_InstalledBaseID { get; set; }
        public int FK_ClientID { get; set; }
        public Nullable<int> FK_EmployeeID { get; set; }
        public Nullable<int> FK_ModuleID { get; set; }
        public Nullable<int> FK_GuarantyID { get; set; }
        public Nullable<int> FK_StatusSchemeID { get; set; }
        public string OrderID { get; set; }
        public string TechnicalID { get; set; }
        public Nullable<bool> PreOrder { get; set; }
        public string URLPreOrder { get; set; }
        public string Symptom { get; set; }
        public string Failure1 { get; set; }
        public string Failure2 { get; set; }
        public string Failure3 { get; set; }
        public string Failure4 { get; set; }
        public string Failure5 { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public System.DateTime OrderCreateDate { get; set; }
        public System.DateTime OrderExecuteDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

        public string OrderFahter { get; set; }
        public string SendCRM { get; set; }
        public Nullable<bool>ExtraKilometres { get; set; }
    }
}
