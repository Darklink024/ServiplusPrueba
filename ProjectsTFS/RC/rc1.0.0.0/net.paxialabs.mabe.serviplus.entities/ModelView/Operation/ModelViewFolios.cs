using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewFolios
    {
        public string TypeFolio { get; set; }
        public int Count { get; set; }
        public string Folio { get; set; }

        public ModelViewFolios(string TypeFolio, int Count, string LastFolio)
        {
            this.TypeFolio = TypeFolio;
            this.Count = Count;
            this.Folio = LastFolio;

        }
    }
}
