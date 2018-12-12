using net.paxialabs.mabe.serviplus.entities.Entity.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace net.paxialabs.mabe.serviplus.web.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IwsChangeStatus" in both code and config file together.
    [ServiceContract]
    public interface IwsChangeStatus
    {
        [OperationContract]
        bool UpdateODS(string orderID, string EstatusEsq, string EstatusCabecera, string tokenApp, string user, string password, out Result wsResult );
    }
}
