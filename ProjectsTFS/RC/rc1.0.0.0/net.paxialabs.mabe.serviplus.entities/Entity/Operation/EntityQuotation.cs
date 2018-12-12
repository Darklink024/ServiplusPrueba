using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityQuotation
    {
        public int PK_QuotationID { get; set; }
        public Nullable<int> FK_OrdenID { get; set; }
        public string SubTotal { get; set; }
        public string IVA { get; set; }
        public string Total { get; set; }
        public string Folio { get; set; }
        public string URL { get; set; }
        public string OrdenVenta { get; set;}
        public bool Status { get; set; }
        public int FK_EmployeeID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> TypeQuotation { get; set; }
    }
}
