using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
     public class EntityRefsellDetail
    {
        public int PK_RefsellDetailID { get; set; }
        public int Fk_QuotationID { get; set; }
        public int Fk_ProductID { get; set; }
        public string Cantidad { get; set; }
        public string RefMan { get; set; }
        public string Origen { get; set; }
        public string CostoRef { get; set; }
        public Nullable<bool> Flete { get; set; }
        public string CostoFlete { get; set; }
        public string OrdenVenta { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
