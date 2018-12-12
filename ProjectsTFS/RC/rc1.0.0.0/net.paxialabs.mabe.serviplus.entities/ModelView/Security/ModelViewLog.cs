using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Security
{
    public class ModelViewLog
    {
        public string TokenUser { get; set; }
        public string TokenApp { get; set; }
        public List<ModelViewDetail> Detail { get; set; }
    }

    public class ModelViewDetail
    { 
    public string TokenLog { get; set; }
    public string OrderID { get; set; }
    public string Module { get; set; }
    public string Message { get; set; }
    public string InnerException { get; set; }
    public string StackTrace { get; set; }
    public string SignType { get; set; }
    public string Battery { get; set; }
    public string SignPercentage { get; set; }
    public string ConnectionType { get; set; }
    public string MobileModel { get; set; }
    public string MobileStorage { get; set; }
   
    public string version { get; set; }
    public string Type { get; set; }
    public string Date { get; set; }
    }
}
