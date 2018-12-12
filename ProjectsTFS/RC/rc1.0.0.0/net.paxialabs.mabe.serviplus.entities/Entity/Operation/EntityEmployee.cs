using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityEmployee
    {
        public int PK_EmployeeID { get; set; }
        public int FK_ModuleID { get; set; }
        public Nullable<int> FK_UserID { get; set; }
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Interlocutor { get; set; }
        public string Society { get; set; }
        public string EmployeeType { get; set; }
        public string StoreProp { get; set; }
        public string DifferentiatorWorkshop { get; set; }
        public string IDPolicy { get; set; }
        public string IDRefSell { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }

    public class EntityEmployeeStore
    {
        public string EmployeeID { get; set; }
        public string InventoryStoreID { get; set; }
        public string InventoryModuleID { get; set; }

    }
}
