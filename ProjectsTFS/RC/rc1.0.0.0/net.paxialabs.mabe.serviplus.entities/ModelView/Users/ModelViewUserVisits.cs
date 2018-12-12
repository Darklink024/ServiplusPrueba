using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Users
{
   public class ModelViewUserVisits
    {
        public string TokenUser { get; set; }
        public string TokenApp { get; set; }
        public DateTime Date { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string ODS { get; set; }
    }
}
