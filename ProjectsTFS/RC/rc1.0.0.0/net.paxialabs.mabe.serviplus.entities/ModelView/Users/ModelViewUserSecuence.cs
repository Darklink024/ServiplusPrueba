using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Users
{
    public class ModelViewUserSecuence
    {
        public string TokenUser { get; set; }
        public string TokenApp { get; set; }
        public List<SecuenceReal> Secuence { get; set; }
    }

    public class  SecuenceReal
    {
        public string OrderID { get; set; }
        public int Secuence { get; set; }
    }
}
