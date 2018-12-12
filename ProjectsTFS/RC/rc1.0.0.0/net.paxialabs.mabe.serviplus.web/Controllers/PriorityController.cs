using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.domain.Facade.Security;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.web.Models.Filters;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class PriorityController : Controller
    {
        [Authorization]
        // GET: Configuration
        public ActionResult Index()
        {
            List<SelectListItem> lst = FacadePriority.GetActives().Select(p => new SelectListItem() { Text = p.Description, Value = p.PK_PriorityID.ToString() }).ToList<SelectListItem>();

            ViewBag.lstPriority = lst;

            return View();
        }


        public ActionResult _List()
        {
            return PartialView();
        }

        public ActionResult _Detail()
        {
            return PartialView();
        }

        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetListAll([DataSourceRequest]DataSourceRequest request)
        {
            var ls = FacadeSchedule.GetListAll();
            return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonErrorHandler]
        public JsonResult savePriority(string modelo)
        {
            ModelViewPriority model = new JavaScriptSerializer().Deserialize<ModelViewPriority>(modelo);
            if (model.ScheduleID == 0)
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Agregar))
                //    return Json("Unauthorized");
                //FacadeUsers.Insert(model);

            }
            else
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Actualizar))
                //    return Json("Unauthorized");
                FacadeSchedule.Update(model);
            }
            return Json("{ Result: 'Success'}");
        }

        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SetStatus_Priority(string PrioritysID)
        {
            try
            {           
                List<int> arr = PrioritysID.Split(',').Select(Int32.Parse).ToList();
                FacadePriority.SetStatus(arr);
                return Json("Solicitud procesada con éxito", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //if (ex.GetType().Name == "Duplicate_Exception") throw ex;
                //if (ex.GetType().Name == "CustomExceptions") throw ex;
                //throw new CustomExceptions(ex, CustomExceptions.ErrorCodes.E_C_Generic);
                throw ex;
            }
        }
    }
}