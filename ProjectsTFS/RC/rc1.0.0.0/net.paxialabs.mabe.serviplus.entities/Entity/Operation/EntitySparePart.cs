using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntitySparePart
    {
        public int PK_SparePartsID { get; set; }
        public int FK_OrderID { get; set; }
        public Nullable<int> FK_BuildOfMaterialsID { get; set; }
        public Nullable<int> FK_ProductID { get; set; }
        public Nullable<int> FK_WorkforceID { get; set; }
        public string RefManID { get; set; }
        public int Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public string PosicionItem { get; set; }
        public string SparePartStatus { get; set; }
        public string StatusSchema { get; set; }
        public string ItemRefMan { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }


    }
}
