using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace net.paxialabs.mabe.serviplus.web.Services
{
    /// <summary>
    /// Summary description for wsUpdateOrder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsUpdateOrder : System.Web.Services.WebService
    {

      [WebMethod]
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
