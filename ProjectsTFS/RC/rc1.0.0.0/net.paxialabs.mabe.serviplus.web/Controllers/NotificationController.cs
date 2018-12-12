using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using net.paxialabs.mabe.serviplus.security.Filters;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class NotificationController : Controller
    {
        [Models.Filters.Authorization]
        // GET: Notification
        public ActionResult Index()
        {
            //FacadeNotification.SendPush(new List<int>() { 1 }, "Hola", "Hole Mike");

            ViewBag.lstModules = FacadeModuleService.GetAll().Select(p => new SelectListItem() { Text = p.ID + " - " + p.Base, Value = p.ModuleID.ToString() }).ToList<SelectListItem>();
            return View();
        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult SetNotificationUser(string data)
        {
            try
            {
                ModelViewNotificationUpdate model = new JavaScriptSerializer().Deserialize<ModelViewNotificationUpdate>(data);
                FacadeNotification.Status(model);
                
                return Json(new ModelViewResultREST() {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                throw new Generic_Exception(ex, Generic_Exception.ErrorCodes.ErrorGoogleMaps);
            }
        }

        [HttpGet]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        public JsonResult GetNotificationUser(string data)
        {
            try
            {
                EntitySecurity model = new JavaScriptSerializer().Deserialize<EntitySecurity>(data);
                var ls = FacadeNotification.GetByToken(model);
                return Json(ls, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Generic_Exception(ex, Generic_Exception.ErrorCodes.ErrorGoogleMaps);
            }
        }
        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetUserAll_Actives(string ModuleID, string Name)
        {
            var ls = FacadeNotification.GetAllNotificationUsers(ModuleID, Name);
            return Json(ls, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [JsonErrorHandler]
        [AllowCrossSiteJson]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetUserAll_Notification(string Employee)
        {
            var ls = FacadeNotification.GetAllNotificationUsers(Employee);
            return Json(ls, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [JsonErrorHandler]
        public JsonResult saveNotification(string notification, int[] Usuarios)
        {
            ModelViewConfiguration model = new JavaScriptSerializer().Deserialize<ModelViewConfiguration>(notification);
            Usuarios = Usuarios.Distinct().ToArray(); 
            if (model.ConfigurationID == 0)
            {
                if(model.Url == "empty")
                {
                    FacadeConfiguration.InsertURLY(model, Usuarios);
                }
                else
               { 
               if (model.UrlType == "youtube")
                {
                 FacadeConfiguration.InsertURLY(model, Usuarios);
                }
                 else if (model.UrlType == "file")
                 {
                   FacadeConfiguration.Insert(Server.MapPath("~/Content/upload/" + model.Url), model, Server.MapPath(GlobalConfiguration.GetLocateNotificationRelative()), Usuarios);
                 }
                }




            }
            else
            {

                if (model.Url == "empty")
                {
                    FacadeConfiguration.UpdateURLY(model, Usuarios);
                }
                else
                {
                    if (model.UrlType == "youtube")
                    {
                        FacadeConfiguration.UpdateURLY(model, Usuarios);
                    }
                    else if (model.UrlType == "file")
                    {
                        FacadeConfiguration.Update(Server.MapPath("~/Content/upload/" + model.Url), model, Server.MapPath(GlobalConfiguration.GetLocateNotificationRelative()), Usuarios);
                    }
                }
            }
            return Json("{ Result: 'Success'}");
        }

        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    //SaveNewUbication(files);
                    // Some browsers send file names with full path. This needs to be stripped.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Content/upload"), fileName);

                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult RemoveImage(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);

                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }


        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetListAll([DataSourceRequest]DataSourceRequest request)
        {
            var ls = FacadeConfiguration.GetListAll();
            return Json(ls.ToDataSourceResult (request), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SetPublish(string ConfigurationIDs)
        {
            try
            {
                List<int> arr = ConfigurationIDs.Split(',').Select(Int32.Parse).ToList();
                foreach (var x in arr)
                {
                    var envio = FacadeReceivers.GetAll().Where(p => p.ConfigurationID == x);
                    List<int> noti = new List<int>();
                    var confi = FacadeConfiguration.GetListAll().Where(p => p.ConfigurationID == x).FirstOrDefault();
                    confi.Publish = true;
                    ModelViewNotification message = new ModelViewNotification();
                    
                    foreach (var y in envio)
                    {
                        noti.Add(y.UserID);
                        message.Message = confi.Message;
                        message.Title = confi.Title;
                        message.Url = confi.Url;
                        message.UserID = y.UserID;
                        message.MessageRead = false;
                        FacadeNotification.Insert(message);
                    }
             
                    FacadeNotification.SendPush(noti, confi.Title, confi.Message);
                    FacadeConfiguration.Update(confi);
                }
               
             
               

                //FacadeAudit.RegisterEntity("/ConfigPromotionTypes/Index", TransactionType.STATUS, "", "", user.IdUser);

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