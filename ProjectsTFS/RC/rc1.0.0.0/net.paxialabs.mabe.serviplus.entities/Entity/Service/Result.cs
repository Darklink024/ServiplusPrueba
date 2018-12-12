using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Service
{
    public class Result
    {
        public bool isComplete { get; set; }
        public string error { get; set; }
        public string detailedError { get; set; }
    }
}
