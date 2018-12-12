using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
    public class EntityGoogleMapsDistanceMatrixRequest
    {
        public string units { get; set; }
        public string origins { get; set; }        
        public string destinations { get; set; }
        public string departure_time { get; set; }
        public string key { get; set; }
    }
}
