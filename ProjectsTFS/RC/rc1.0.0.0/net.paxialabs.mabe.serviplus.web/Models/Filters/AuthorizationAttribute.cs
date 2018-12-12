using net.paxialabs.mabe.serviplus.domain.Facade.Security;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Mvc;

namespace net.paxialabs.mabe.serviplus.web.Models.Filters
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)


       {

            //filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

            var UserToken = filterContext.RequestContext.HttpContext.Request.Cookies["ServiPlus"];

            if (UserToken == null)

            { filterContext.Result = new RedirectResult("/Security/Index"); }

            else
            {
            var usrData = FacadeUsers.GetUserByToken(UserToken.Values["TokenUser"]);

                var ruta = filterContext.HttpContext.Request.Url.AbsolutePath;
                if(ruta == "/") { ruta = "/Home/Index"; }

                var access = (from a in FacadeModule.GetAll().Where(p => p.URL == ruta)
                              join b in FacadePermission.GetAll(usrData.ProfileID) on a.ModuleID equals b.ModuleID
                              select new ModelViewPermission
                              {
                                  ModuleID = a.ModuleID,
                                  ProfileID = b.ProfileID,
                                  Module = a.Module,
                                  URL = a.URL,
                                  Access = b.Access
                              }).First();

                if (!access.Access)
                {
                    filterContext.Result = new RedirectResult("/Security/Error");
                }
                else
                {
                    if (usrData == null) filterContext.Result = new RedirectResult("/Security/Index");
                    filterContext.Controller.ViewBag.Profile = FacadeProfile.GetAll().Where(p => p.ProfileID == usrData.ProfileID).Single().Profile;
                    filterContext.Controller.ViewBag.User = usrData.Name;
                    filterContext.Controller.ViewBag.Module = "Global";
                }

                //if (usrData == null) filterContext.Result = new RedirectResult("/Security/Index");
                //filterContext.Controller.ViewBag.Profile = FacadeProfile.GetAll().Where(p => p.ProfileID == usrData.ProfileID).Single().Profile;
                //filterContext.Controller.ViewBag.User = usrData.Name;
                //filterContext.Controller.ViewBag.Module = "Global";

            }
        }

    }
}