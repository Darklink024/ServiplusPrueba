using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using net.paxialabs.mabe.serviplus.domain.Facade.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.security.Filters;
using net.paxialabs.mabe.serviplus.web.Models.Filters;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using net.paxialabs.mabe.serviplus.domain.Facade.Interface;
using net.paxialabs.mabe.serviplus.domain.Business.Interface;
using System.Globalization;
using net.paxialabs.mabe.serviplus.security;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;

namespace net.paxialabs.mabe.serviplus.web.Controllers
{
    public class SecurityController : Controller
    {
        [Models.Filters.Authorization]
        #region Profile
        // GET: Security
        public ActionResult Profiles()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }


        public ActionResult _ProfileList()
        {
            return PartialView();
        }

        public ActionResult _ProfileDetail()
        {
            return PartialView();
        }

        [JsonErrorHandler]
        public ActionResult _Login()
        {
            return PartialView();
        }

        [JsonErrorHandler]
        public ActionResult _Recovery()
        {
            return PartialView();
        }

        [JsonErrorHandler]
        public ActionResult _ChangePassword()
        {
            return PartialView();
        }

        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetAllList_Profile([DataSourceRequest]DataSourceRequest request)
        {
            // if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Lectura))
            //     throw new Exception("Sin acceso");

            var ls = FacadeProfile.GetAll();
            return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonErrorHandler]
        public JsonResult saveProfile(string modelo)
        {
            ModelViewProfile model = new JavaScriptSerializer().Deserialize<ModelViewProfile>(modelo);
            if (model.ProfileID == 0)
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Agregar))
                //  return Json("Unauthorized");
                FacadeProfile.Insert(model);

            }
            else
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Actualizar))
                //    return Json("Unauthorized");
                FacadeProfile.Update(model);
            }
            return Json("{ Result: 'Success'}");
        }

        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SetStatus_Profiles(string ProfilesIDs)
        {
            try
            {
                //var user = (User)Session["Usuario"];

                List<int> arr = ProfilesIDs.Split(',').Select(Int32.Parse).ToList();
                FacadeProfile.SetStatus(arr);

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

        #endregion

        #region Module
        public ActionResult Modules()
        {
            List<SelectListItem> arr = new List<SelectListItem>();
            arr.Add(new SelectListItem() { Text = "Mobile", Value = "Mobile" });
            arr.Add(new SelectListItem() { Text = "Web", Value = "Web" });
            ViewBag.lstSection = arr;

            return View();
        }


        public ActionResult Password()
        {
            //HttpCookie myCookieobj = new HttpCookie("ServiPlus");

            HttpCookie myCookie = new HttpCookie("ServiPlus");
            myCookie = Request.Cookies["ServiPlus"];

            ViewBag.UserToken = myCookie["TokenUser"];
            ViewBag.AppToken = GlobalConfiguration.TokenWEB;
            return View();
        }

        public ActionResult _ModuleList()
        {
            return PartialView();
        }

        public ActionResult _ModuleDetail()
        {          
            return PartialView();
        }

        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetAllList_Module([DataSourceRequest]DataSourceRequest request)
        {
            // if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Lectura))
            //     throw new Exception("Sin acceso");

            var ls = FacadeModule.GetAll();
            return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonErrorHandler]
        public JsonResult saveModule(ModelViewModule model)
        {
            if (model.ModuleID == 0)
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Agregar))
                //    return Json("Unauthorized");
                FacadeModule.Insert(model);

            }
            else
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Actualizar))
                //    return Json("Unauthorized");
                FacadeModule.Update(model);
            }
            return Json("{ Result: 'Success'}");
        }

        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SetStatus_Modules(string ModuleIDs)
        {
            try
            {
                //var user = (User)Session["Usuario"];

                List<int> arr = ModuleIDs.Split(',').Select(Int32.Parse).ToList();
                FacadeModule.SetStatus(arr);

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

        #endregion

        #region Permission
        public ActionResult Permissions()
        {
            List<SelectListItem> lst = FacadeProfile.GetActives().Select(p => new SelectListItem() { Text = p.Profile, Value = p.ProfileID.ToString() }).ToList<SelectListItem>();
            ViewBag.lstPerfil = lst;

            return View();
        }

        public ActionResult _PermissionList()
        { 
            return PartialView();
        }

        public ActionResult _PermissionDetail()
        {
            return PartialView();
        }

        [HttpPost()]
        [JsonErrorHandler]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetAllList_Permission([DataSourceRequest]DataSourceRequest request, int ProfileID)
        {
            // if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Lectura))
            //     throw new Exception("Sin acceso");

            var ls = FacadePermission.GetAll(ProfileID);
            return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonErrorHandler]
        public JsonResult savePermission(ModelViewPermission model)
        {

            //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Actualizar))
            //    return Json("Unauthorized");
            FacadePermission.Update(model);
            
            return Json("{ Result: 'Success'}");
        }

        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SetStatus_Permission(string ModuleIDs)
        {
            try
            {
                //var user = (User)Session["Usuario"];

                List<int> arr = ModuleIDs.Split(',').Select(Int32.Parse).ToList();
                FacadeModule.SetStatus(arr);

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
        #endregion

        #region Authenticate

        // GET: Security
        public ActionResult Index()
        {
            ViewBag.AppToken = GlobalConfiguration.TokenWEB;
            
            return View();
        }

        [HttpGet]
        [AllowCrossSiteJson]
        [JsonErrorHandler]
        public JsonResult Authenticate(string session)
        {
            try
            {
               
                ModelViewLogin objCred = new JavaScriptSerializer().Deserialize<ModelViewLogin>(session);
                var lst = FacadeSecurity.Authenticate(objCred);
                HttpCookie myCookieobj = new HttpCookie("ServiPlus");
                myCookieobj["TokenUser"] = lst.Token;
                myCookieobj.Expires = DateTime.Now.Add(TimeSpan.FromDays(365));
                Response.Cookies.Add(myCookieobj);
                
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;  
            }
        }

        [HttpGet]
        [AllowCrossSiteJson]
        [JsonErrorHandler]
        public ActionResult Recovery(string Data)
        {
            try
            {
                ViewBag.AppToken = GlobalConfiguration.TokenWEB;

                if (Data != null)
                {
                    ModelViewRecovery objCred = new JavaScriptSerializer().Deserialize<ModelViewRecovery>(Data);                    
                    FacadeSecurity.Recovery(objCred);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [AllowCrossSiteJson]
        [JsonErrorHandler]
        public JsonResult ChangePassword(string Data)
        {
            try
            {
                ModelViewChangePassword objCred = new JavaScriptSerializer().Deserialize<ModelViewChangePassword>(Data);
                FacadeSecurity.ChangePassword(objCred);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult SingOut()
        {        
            HttpCookie myCookie = new HttpCookie("ServiPlus");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);

            return View();
        }

        
        public ActionResult File()
        {
            try
            {
                string DownloadFolder = "";
                List<string> arrFiles = new List<string>();
                List<string> arrFilesOK = new List<string>();
                //FacadeInterface.Download(true, out arrFiles, out arrFilesOK, out DownloadFolder);
                FacadeInterface.Process(DownloadFolder);

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult FileDEV()

        {
            try
            {
                FacadeInterface.Files();
                //FacadeInterface.Import(GlobalConfiguration.MabeSFTPFolderLocal);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DownloadFile()
        {
            try
            {
                string DownloadFolder = "";
                List<string> arrFiles = new List<string>();
                List<string> arrFilesOK = new List<string>();
                //FacadeInterface.Download(true, out arrFiles, out arrFilesOK, out DownloadFolder);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
        public ActionResult Test(string DataIn, string DateFn)
        {
            try
            {
                               
                string message = "BillingDetails";

                var ls = FacadeSecurity.GetBillings(DataIn, DateFn, message);
                 
                foreach (var item in ls)
                {
                    var usuario = item.FK_UserID.Value;
                    ModelViewUserG objCred = new ModelViewUserG();

                    var user = FacadeUsers.GetByID(item.FK_UserID.Value);

                    objCred.TokenApp = "26438b8b-a543-4d16-9ba6-8c7049a5c7cf";
                    objCred.TokenUser = user.Token.ToString();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string value = js.Serialize(objCred);

                    var orden= new OrderController().SetBilling2(value, item.Message, item.Date.Value);





                }

                      

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}