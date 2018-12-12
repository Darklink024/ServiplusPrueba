using System;
using System.Linq;
using System.Web.Mvc;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System.Web.Script.Serialization;
using net.paxialabs.mabe.serviplus.security.Filters;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.domain.Facade.Interface;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons;
using System.Collections.Generic;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using System.Net;
using System.IO;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.domain.Facade.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Interface;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetAll()
        {
            try
            {

                var lt = FacadePolicy.GetAll();                
                return Json(lt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //[HttpPost]
        //[JsonErrorHandler]
        //[AllowCrossSiteJson]
        //public JsonResult SendNotification()
        //{
        //    try
        //    {

        //        FacadePolicy.SendSMS();
        //        return Json("OK", JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}





    }
}
