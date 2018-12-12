using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.resource.Operation;
using net.paxialabs.mabe.serviplus.web.Models.Filters;

using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class MonitorController : Controller
    {
        [Authorization]
        // GET: Monitor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Evidence()
        {
            return View();
        }

        [JsonErrorHandler]
        public ActionResult _Photo()
        {
            return PartialView();
        }

        [JsonErrorHandler]
        public ActionResult GetList_Modules()
        {
            var Cookie = Request.Cookies["ServiPlus"];
            var Token = Cookie.Value;
            string[] nums = Token.Split('=').ToArray();
            string User = nums[1];
            var user = FacadeUsers.GetUserByToken(User);
            if (user.ProfileID == 7)
            {

                var lsTA = FacadeModuleService.GetAllBYModuleList(user.ModuleID.Value).Select(p => new SelectListItem() { Text = p.ID + " - " + p.Base, Value = p.ModuleID.ToString() }).ToList<SelectListItem>();
                

                return Json(lsTA, JsonRequestBehavior.AllowGet);
            }
            else
           {

            var ls = FacadeModuleService.GetAll().Select(p => new SelectListItem() { Text = p.ID + " - " + p.Base, Value = p.ModuleID.ToString() }).ToList<SelectListItem>();
            return Json(ls, JsonRequestBehavior.AllowGet);
            }

        }
        [JsonErrorHandler]
        public ActionResult GetList_StatusOrder()
        {
            var ls = FacadeStatusCauseOrder.GetAll().Select(p => new SelectListItem() { Text = p.StatusOrder1, Value = p.PK_StatusOrderID.ToString() }).ToList<SelectListItem>();
            return Json(ls, JsonRequestBehavior.AllowGet);

        }
        [JsonErrorHandler]
        public ActionResult GetList_StatusVisit()
        {
            var Manager = new ResourceManager("net.paxialabs.mabe.serviplus.resource.Operation.ResourceStatusVisit", typeof(ResourceStatusVisit).Assembly);
            var ls = FacadeStatusCauseVisit.GetAll().Select(p => new SelectListItem() {
                Text = Manager.GetString(p.StatusVisit1), Value = p.PK_StatusVisitID.ToString() }).ToList<SelectListItem>();
            return Json(ls, JsonRequestBehavior.AllowGet);

        }

        [JsonErrorHandler]
        public ActionResult GetList_ServiceType()
        {
            var Cookie = Request.Cookies["ServiPlus"];
            var Token = Cookie.Value;
            string[] nums = Token.Split('=').ToArray();
            string User = nums[1];
            var user = FacadeUsers.GetUserByToken(User);

            if (user.ProfileID == 7)
            {
                var ls = FacadeGuaranty.GetAll().Select(p => new SelectListItem()
                {
                    Text = p.GuarantyID + " - " + p.Guaranty1,
                    Value = p.PK_GuarantyID.ToString(),
                }).Where(p => p.Text != "0070 - SERVICIO DE CARGO" && p.Text != "0080 - DEMOSTRACIÓN/CONEXIÓN-CARGO" && p.Text != "0090 - INSTALACIÓN DE PRODUCTO-CARGO").ToList<SelectListItem>();

                return Json(ls, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ls = FacadeGuaranty.GetAll().Select(p => new SelectListItem()
                {
                    Text = p.GuarantyID + " - " + p.Guaranty1,
                    Value = p.PK_GuarantyID.ToString()
                }).ToList<SelectListItem>();
                return Json(ls, JsonRequestBehavior.AllowGet);
            }
        }
        [JsonErrorHandler]
        public ActionResult GetList_TypeQuotation()
        {
            var ls = FacadeTypeQuotation.GetAll().Select(p => new SelectListItem()
            {

                Text = p.Description,
                Value = p.PK_TypeQuotation.ToString()
            }).ToList<SelectListItem>();
            return Json(ls, JsonRequestBehavior.AllowGet);
        }

       
        [JsonErrorHandler]
        public ActionResult GetList_UserTA(int monitorID)
        {

            var dataMonitor = FacadeMonitor.Get(monitorID);
           

            if (dataMonitor.StatusVisitID == 1)
            {
                var dataODS = FacadeOrder.Get(dataMonitor.OrderID);
                var dataEmployee = FacadeEmployee.GetID(dataODS.FK_EmployeeID.Value);
                var dataUser = FacadeUsers.GetByID(dataEmployee.FK_UserID.Value);
                var ls = FacadeUsers.GetAllUserModuleProfile(dataEmployee.FK_ModuleID, dataUser.ProfileID).Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.UserID.ToString()
                }
                ).Where(p => p.Text != dataUser.Name).ToList<SelectListItem>();
                return Json(ls, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = "error";
                return Json(result);
            }
            
        }
        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SaveAsig(int Newuser, int order)
        {
            var datauser= FacadeEmployee.GetEmployeeByUser(Newuser);

            var dataMonitor = FacadeMonitor.Get(order);
            var dataODS = FacadeOrder.Get(dataMonitor.OrderID);
            var EmployeeID = datauser[0].PK_EmployeeID;
            var OrderID = dataODS.PK_OrderID;
             FacadeOrder.SetUser(OrderID, EmployeeID);

                      
           var result = "Success";

            return Json(result);
            

        }
        [JsonErrorHandler]
        public ActionResult GetList_Prioritys()
        {
            var ls = FacadeSchedule.GetAll().Select(p => new SelectListItem() { Text = p.ScheduleStart + " - " + p.ScheduleEnd, Value = p.PK_ScheduleID.ToString() }).ToList<SelectListItem>();
            return Json(ls, JsonRequestBehavior.AllowGet);

        }

        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetListVisitAll([DataSourceRequest]DataSourceRequest request,string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee,string StartDate, string EndDate)
        {
            
            var Cookie = Request.Cookies["ServiPlus"];
            var Token = Cookie.Value;
            string [] nums = Token.Split('=').ToArray();
            string User = nums[1];
            var user = FacadeUsers.GetUserByToken(User);
         
            var ls = FacadeMonitor.GetList(StatusVisitID ,ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, user);
            return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);
        }

        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetLisEvidencetAll([DataSourceRequest]DataSourceRequest request, string StatusVisitID, string ModuleID, string PriorityID, string StatusOrderID, string ServiceID, string OrderID, string Employee, string StartDate, string EndDate)
        {
            var Cookie = Request.Cookies["ServiPlus"];
            var Token = Cookie.Value;
            string[] nums = Token.Split('=').ToArray();
            string User = nums[1];
            var user = FacadeUsers.GetUserByToken(User);

            var ls = FacadeOrderEvidence.GetLisEvidencetAll(StatusVisitID,ModuleID, PriorityID, StatusOrderID, ServiceID, OrderID, Employee, StartDate, EndDate, user);
            return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SetStatus_CRM(string OrderID)
        {
            try
            {
                FacadeOrder.SetStatus(OrderID);
                return Json("Solicitud procesada con éxito", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        public ActionResult GetList_Address(string OrderID)
        {
            var ls = FacadeOrder.GetList_Address(OrderID);
            return Json(ls, JsonRequestBehavior.AllowGet);

        }

    }
}