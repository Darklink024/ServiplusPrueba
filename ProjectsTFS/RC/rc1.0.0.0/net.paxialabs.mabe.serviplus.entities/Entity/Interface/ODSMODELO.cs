using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
    public class ODSMODELO
    {
        public string Modelo { get; set; }
        public List<Falla> Fail { get; set; }
    }
    public class Falla
    {
        public string IDFalla { get; set; }
        public string DescripcionFalla { get; set; }
        public string Complejidad { get; set; }
    }
}
