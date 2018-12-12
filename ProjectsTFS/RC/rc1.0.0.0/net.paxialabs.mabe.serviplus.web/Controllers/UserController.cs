using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using net.paxialabs.mabe.serviplus.domain.Facade.Security;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
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
    public class UserController : Controller
    {
        [Authorization]
        // GET: User
        public ActionResult Index()
        {
            List<SelectListItem> lst = FacadeProfile.GetActives().Select(p => new SelectListItem() { Text = p.Profile, Value = p.ProfileID.ToString() }).ToList<SelectListItem>();

            ViewBag.lstPerfil = lst;

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
        public ActionResult GetAllList_Users([DataSourceRequest]DataSourceRequest request)
        {
            var Cookie = Request.Cookies["ServiPlus"];
            var Token = Cookie.Value;
            string[] nums = Token.Split('=').ToArray();
            string User = nums[1];
            var user = FacadeUsers.GetUserByToken(User);
            if (user.ProfileID == 7)
            {

                var ls = FacadeUsers.GetUserByModule(user.ModuleID.Value);
                return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);

            }
            else
            {
                // if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Lectura))
                //     throw new Exception("Sin acceso");

                var ls = FacadeUsers.GetAll();
                return Json(ls.ToDataSourceResult(request), JsonRequestBehavior.DenyGet);

            }
            
        }

        [HttpPost]
        [JsonErrorHandler]
        public JsonResult saveUser(string modelo)
        {
            ModelViewUser model = new JavaScriptSerializer().Deserialize<ModelViewUser>(modelo);
            if (model.UserID == 0)
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Agregar))
                //    return Json("Unauthorized");
                FacadeUsers.Insert(model);

            }
            else
            {
                //if (!FachadaSeguridad.Autorizado(HttpContext.User.Identity.Name, "/Usuario/Index", AuditoriaAccion.Actualizar))
                //    return Json("Unauthorized");
                FacadeUsers.Update(model);
            }
            ModelViewResultREST result = new ModelViewResultREST();
            result.Result = "Success";

            return Json(result);
        }

        [HttpPost]
        [JsonErrorHandler]
        public JsonResult registerFCM(string data)
        {
            try
            {
                ModelViewUserFCM model = new JavaScriptSerializer().Deserialize<ModelViewUserFCM>(data);
                FacadeUsers.RegisterFCM(model);
                ModelViewResultREST result = new ModelViewResultREST();
                result.Result = "Success";

                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }


        [HttpPost]
        [JsonErrorHandler]
        public ActionResult SetStatus_Users(string UserIDs)
        {
            try
            {
                //var user = (User)Session["Usuario"];

                List<int> arr = UserIDs.Split(',').Select(Int32.Parse).ToList();
                FacadeUsers.SetStatus(arr);
                
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