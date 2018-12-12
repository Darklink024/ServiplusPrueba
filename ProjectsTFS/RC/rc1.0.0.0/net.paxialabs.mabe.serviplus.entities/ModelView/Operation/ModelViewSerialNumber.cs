using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewSerialNumber
    {
        public int ModelSerialNumberID { get; set; }
        public int ProductID { get; set; }
        public string Model { get; set; }
        public string ValidationFormatID { get; set; }
        public bool ValidDate { get; set; }
        public string InitialDate { get; set; }
        public string EndDate { get; set; }
        public int ValidationsSerialNumberID { get; set; }
        public string ValidationName { get; set; }
        public int InitialPosition { get; set; }
        public int FinalPosition { get; set; }
        public string Allowed { get; set; }
        public string RankID { get; set; }

    }
}
