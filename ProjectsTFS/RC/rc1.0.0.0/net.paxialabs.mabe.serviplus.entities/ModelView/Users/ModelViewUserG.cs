using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Users
{
   public class ModelViewUserG
    {
        public string TokenUser { get; set; }
        public string TokenApp { get; set; }
        public string Model { get; set; }
        public int  ProductID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public int Page { get; set; }
        public int Size { get; set; }

    }
}
