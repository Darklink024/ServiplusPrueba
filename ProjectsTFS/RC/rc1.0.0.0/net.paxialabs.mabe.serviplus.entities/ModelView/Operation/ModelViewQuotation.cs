using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewQuotation
    {
        public int FK_OrderID { get; set; }
        public int FK_CauseOrderID { get; set; }
        public int QuotationID { get; set; }
        public string OrderID { get; set; }
        public int StatusQuotation { get; set; }
        public string Folio { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
        public string Total { get; set; }
        public string URL { get; set; }
        public string Contract { get; set; }
        public string TypeQuotation { get; set; }
        public string OrdenVenta { get; set; }
    }
}
