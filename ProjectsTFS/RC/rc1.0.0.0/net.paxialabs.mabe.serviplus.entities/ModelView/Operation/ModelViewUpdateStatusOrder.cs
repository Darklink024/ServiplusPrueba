using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewUpdateStatusOrder : EntitySecurity
    {
        public string ODS { get; set; }
        public int Secuence { get; set; }
        public string Status { get; set; }
        public string Latitude { get; set; }
        public string Logitude { get; set; }
        public DateTime? StartVisitDate { get; set; }
        public List<SparePart> SpareParts { get; set; }
    }
}
