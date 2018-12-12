using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityModuleService
    {
        public int ModuleID { get; set; }
        public string ID { get; set; }
        public string Region { get; set; }
        public string Module { get; set; }
        public string Base { get; set; }
        public string AddressStreet { get; set; }
        public Nullable<int> AddressNumber { get; set; }
        public  Nullable<int> AddressCP { get; set; }
        public string AddressColony { get; set; }
        public string AddressCity { get; set; }
        public Single? LatWorkshop { get; set; }
        public Single? LongWorkshop { get; set; }
        public Nullable<bool> AutorizedWorkshop { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        
    }
}
