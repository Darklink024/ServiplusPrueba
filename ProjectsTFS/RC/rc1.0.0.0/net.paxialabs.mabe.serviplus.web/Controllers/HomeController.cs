using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.domain.Facade.Security;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using net.paxialabs.mabe.serviplus.web.Models.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Device;
using System.Web.Mvc;
using System.Device.Location;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class HomeController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            ViewBag.lstModules = FacadeModuleService.GetAll().Select(p => new SelectListItem() { Text = p.ID + " - " + p.Base, Value = p.ModuleID.ToString() }).ToList<SelectListItem>();
            ViewBag.lstPrioritys = FacadeSchedule.GetAll().Select(p => new SelectListItem() { Text = p.ScheduleStart + " - " + p.ScheduleEnd, Value = p.PK_ScheduleID.ToString() }).ToList<SelectListItem>();
            ViewBag.lstGuaranty = FacadeGuaranty.GetAll().Select(p => new SelectListItem() { Text = p.GuarantyID + " - " + p.Guaranty1, Value = p.PK_GuarantyID.ToString() }).ToList<SelectListItem>();
            ViewBag.lstStatusOrder = FacadeStatusCauseOrder.GetAll().Select(p => new SelectListItem() { Text = p.StatusOrder1 , Value = p.PK_StatusOrderID.ToString() }).ToList<SelectListItem>();

            return View();
        }
       
        public ActionResult _DetailODS(int MonitorID)
        {
            var dataMonitor = FacadeMonitor.Get(MonitorID);
            var dataODS = FacadeOrder.Get(dataMonitor.OrderID);
            var dataTecnico = FacadeEmployee.GetID(dataODS.FK_EmployeeID.Value);
            ViewBag.dataODS = dataODS;
            ViewBag.dataMonitor = dataMonitor;
            ViewBag.dataTecnico = dataTecnico;
            
            return PartialView();
        }
        public ActionResult _ODSAssignament(int MonitorID)
        {
            var dataMonitor = FacadeMonitor.Get(MonitorID);
            var dataODS = FacadeOrder.Get(dataMonitor.OrderID);
            var dataModule = FacadeModuleService.GetAllBYModule(dataODS.FK_ModuleID.Value);
            var dataTecnico = FacadeEmployee.GetID(dataODS.FK_EmployeeID.Value);
            ViewBag.dataODS = dataODS;
            ViewBag.dataMonitor = dataMonitor;
            ViewBag.dataTecnico = dataTecnico;
            ViewBag.monitorID = MonitorID;
            ViewBag.dataModule = dataModule;
            return PartialView();
        }

        public ActionResult _DetailODSCP(int OrderID)
        {
            var dataODS = FacadeOrder.Get(OrderID);
            var dataClient = FacadeClient.GetByID(dataODS.FK_ClientID);
            var dataBI = FacadeInstalledBase.GetByID(dataODS.FK_InstalledBaseID);
            
            ViewBag.dataODS = dataODS;
            ViewBag.dataClient = dataClient;
            return PartialView();
        }

        public ActionResult _DetailODSIB(int OrderID)
        {
            var dataODS = FacadeOrder.Get(OrderID);            
            var dataIB = FacadeInstalledBase.GetByID(dataODS.FK_InstalledBaseID);
            var dataProduct = FacadeProduct.GetByID(dataIB.FK_ProductID.Value);
            var dataShopPlace = FacadeShopPlace.Get(dataIB.FK_ShopPlaceID.Value);

            ViewBag.dataODS = dataODS;
            ViewBag.dataIB = dataIB;
            ViewBag.dataProduct = dataProduct;
            ViewBag.dataShopPlace = dataShopPlace;

            return PartialView();
        }

        public ActionResult _DetailODSFR(int OrderID)
        {
            var dataODS = FacadeOrder.Get(OrderID);
            var dataRef = FacadeSparePart.GetByOrderID(OrderID);

            string desc1 = "", desc2 = "", desc3 = "", desc4 = "", desc5 = "";

            if (!string.IsNullOrEmpty(dataODS.Failure1)) desc1 = FacadeCodeFailure.GetByCodeFailure(dataODS.Failure1).Failure;
            if (!string.IsNullOrEmpty(dataODS.Failure2)) desc2 = FacadeCodeFailure.GetByCodeFailure(dataODS.Failure2).Failure;
            if (!string.IsNullOrEmpty(dataODS.Failure3)) desc3 = FacadeCodeFailure.GetByCodeFailure(dataODS.Failure3).Failure;
            if (!string.IsNullOrEmpty(dataODS.Failure4)) desc4 = FacadeCodeFailure.GetByCodeFailure(dataODS.Failure4).Failure;
            if (!string.IsNullOrEmpty(dataODS.Failure5)) desc5 = FacadeCodeFailure.GetByCodeFailure(dataODS.Failure5).Failure;


            ViewBag.desc1 = desc1;
            ViewBag.desc2 = desc2;
            ViewBag.desc3 = desc3;
            ViewBag.desc4 = desc4;
            ViewBag.desc5 = desc5;

            var dataGuranty = FacadeGuaranty.Get(dataODS.FK_GuarantyID.Value);
           
            

            ViewBag.dataODS = dataODS;
            ViewBag.dataRef = dataRef;
            ViewBag.dataGuranty = dataGuranty;


            return PartialView();
        }

        public ActionResult _DetailODSNotes(int OrderID)
        {
            
            var dataODS = FacadeOrder.Get(OrderID);
            var dataVisit = FacadeVisit.GetByOrderID(OrderID);
            ViewBag.dataODS = dataODS;
            ViewBag.dataVisit = dataVisit;
  
            return PartialView();
        }

        public ActionResult _DetailODSLog(int OrderID)
        {
            var dataLog = FacadeLogCRM.GetByOrderID(OrderID);
            if(dataLog== null) { dataLog = new entities.Entity.Operation.EntityLogCRM(); }
            ViewBag.dataLog = dataLog;

            return PartialView();
        }
        
        public ActionResult _DetailODSIP(int OrderID)
        {
            var dataODS = FacadeOrder.Get(OrderID);
            var dataInvoicing = FacadeInvoicing.GetPolicyInvoice(OrderID,"");
            var dataPayment = FacadePayment.GetPaymetByType(OrderID);

            ViewBag.dataODS = dataODS;
            ViewBag.dataInvoicing = dataInvoicing;
            ViewBag.dataPayment = dataPayment;
            
            return PartialView();
        }

        public ActionResult _DetailODSResumen(int OrderID)
        {
            var dataODS = FacadeOrder.Get(OrderID);

            ViewBag.dataODS = dataODS;

            return PartialView();
        }
        public ActionResult _DetailDistance(int orderID)
        {
            string refMan= "8011161600000025";
            var dataMonitor = FacadeMonitor.GetByOrderID(orderID);
            var dataODS = FacadeOrder.Get(orderID);
            var dataModule = FacadeModuleService.GetAllBYModule(dataODS.FK_ModuleID.Value);
            var dataSparePart = FacadeSparePart.GetByRefManID(orderID, refMan);
            if (dataSparePart == null)
            {
                ViewBag.Kilometres = 0;

            }
            else
            {
                ViewBag.Kilometres = dataSparePart.Quantity;
            }
            ViewBag.dataODS = dataODS;
            ViewBag.dataMonitor = dataMonitor;
            ViewBag.dataModule = dataModule;
            
           
            return PartialView();
        }
    }
 }