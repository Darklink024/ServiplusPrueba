using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityConfiguration
    {
        public int ConfigurationID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public bool Status { get; set; }
        public bool Publish { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        
    }
}
