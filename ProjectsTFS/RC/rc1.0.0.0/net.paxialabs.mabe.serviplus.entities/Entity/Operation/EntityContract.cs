using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityContract
    {
        public int PK_ContractID { get; set; }
        public int Fk_OrderID { get; set; }
        
        public string Folio { get; set; }
         public string Ruta { get; set; }

        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }

    }
}
