using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using net.paxialabs.mabe.serviplus.entities.Entity.Service;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;

namespace net.paxialabs.mabe.serviplus.web.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wsChangeStatus" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select wsChangeStatus.svc or wsChangeStatus.svc.cs at the Solution Explorer and start debugging.
    public class wsChangeStatus : IwsChangeStatus
    {
        
        public bool UpdateODS(string orderID, string EstatusEsq, string EstatusCabecera, string tokenApp, string user, string password, out Result wsResult)
        {
            wsResult = new Result();
            
            try
            {
                FacadeOrder.UpdateStatusOrderWS(orderID, EstatusEsq, EstatusCabecera,  tokenApp, user, password);

                wsResult.isComplete = true;
                wsResult.error = "SIN_ERROR";
                wsResult.detailedError = "SIN_ERROR";

                return true;
            }
            catch (Exception ex)
            {
                wsResult.isComplete = false;
                wsResult.error = ex.Message;
                wsResult.detailedError = ex.StackTrace;

                return false;
            }
        }
    }
}
