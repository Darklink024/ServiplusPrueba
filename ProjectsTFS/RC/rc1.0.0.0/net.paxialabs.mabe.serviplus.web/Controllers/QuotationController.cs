using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.web.Models.Filters;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class QuotationController : Controller
    {
        [Authorization]
        // GET: Quotation
        public ActionResult Index()
        {
            //ViewBag.lstModules = FacadeModuleService.GetAll().Select(p => new SelectListItem() { Text = p.ID + " - " + p.Base, Value = p.ModuleID.ToString() }).ToList<SelectListItem>();
            //ViewBag.lstPrioritys = FacadeSchedule.GetAll().Select(p => new SelectListItem() { Text = p.ScheduleStart + " - " + p.ScheduleEnd, Value = p.PK_ScheduleID.ToString() }).ToList<SelectListItem>();
            //ViewBag.lstGuaranty = FacadeGuaranty.GetAll().Select(p => new SelectListItem() { Text = p.GuarantyID + " - " + p.Guaranty1, Value = p.PK_GuarantyID.ToString() }).ToList<SelectListItem>();
            //ViewBag.lstStatusOrder = FacadeStatusCauseOrder.GetAll().Select(p => new SelectListItem() { Text = p.StatusOrder1, Value = p.PK_StatusOrderID.ToString() }).ToList<SelectListItem>();

            return View();
        }

        public ActionResult _List()
        {
            return PartialView();
        }


        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetListAllQuotation([DataSourceRequest]DataSourceRequest request, string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate, string QuotationID)
        {
            var Cookie = Request.Cookies["ServiPlus"];
            var Token = Cookie.Value;
            string[] nums = Token.Split('=').ToArray();
            string User = nums[1];
            var user = FacadeUsers.GetUserByToken(User);
            
            var ls = FacadeMonitor.GetListAll(StatusVisitID,ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, User,QuotationID);
            return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);
        }
    }
}