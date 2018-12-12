using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewReceiptRef
    {
        public string FK_OrderID { get; set; }
        public string Folio { get; set; }
        public string IVA { get; set; }
        public string SubTotal { get; set; }
        public string ClientID { get; set; }
        public string Total { get; set; }
        public string PhoneNumber { get; set; }
        public string EMails { get; set; }
        public int ProductID { get; set; }
        public List<SparePartre> SpareParts { get; set; }
    }
    public class  SparePartre
    {
       
        
        public int Quantity { get; set; }
        public string Totals { get; set; }
        public string Price { get; set; }
        public string RefManID { get; set; }
        public string SparePartsDescription { get; set; }

    }
}
