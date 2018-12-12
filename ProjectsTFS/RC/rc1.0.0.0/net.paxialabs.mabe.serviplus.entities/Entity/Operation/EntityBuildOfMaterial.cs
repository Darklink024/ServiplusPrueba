using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityBuildOfMaterial
    {
        public int PK_BuildOfMaterialsID { get; set; }
        public int FK_ProductID { get; set; }
        public string Model { get; set; }
        public string SparePartsID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string StatusBOM { get; set; }
        public string SparePartDescription { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
