using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewFailures
    {
        public int CodeFailureID { get; set; }
        public int ProductID { get; set; }
        public string CodeFailure { get; set; }
        public string Failure { get; set; }
        public int Complexity { get; set; }
        public bool Status { get; set; }
        //public List<Fail> FailureByProduct { get; set; }
    }

    //public class Fail
    //{
    //    public int ProductID { get; set; }
    //    public int Complexity { get; set; }
    //    //public string Failure { get; set; }
    //    public bool Status { get; set; }
    //}
}
