using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
    public class EntityREFACCIONES
    {
        public int RefaccionID { get; set; }
        public string ID_REF { get; set; }
        public string CENTRO { get; set; }
        public string ALMACEN { get; set; }
        public Nullable<int> TOTDISP { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> Creacion { get; set; }
        public Nullable<System.DateTime> Modificacion { get; set; }
    }
}
