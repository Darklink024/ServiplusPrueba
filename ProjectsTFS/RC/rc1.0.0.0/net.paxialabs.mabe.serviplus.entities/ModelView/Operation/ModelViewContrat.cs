using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewContrat
    {
        public string FK_OrderID { get; set; }
        public string Folio { get; set; }
        public string Period { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NumbClient { get; set;}
        public string Town { get; set; }
        public string Totals { get; set; }        
        public string Model { get; set; }
        public string Description { get; set; }
        public string Serie { get; set; }
        public string PhoneNumber { get; set; }
        public string EMails { get; set; }
        

    }
}
