using System;
using System.Web;
using System.Web.Mvc;

namespace net.paxialabs.mabe.serviplus.security.Filters
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var UserToken = filterContext.RequestContext.HttpContext.Request.Cookies["ServiPlus"];

            if (UserToken == null) filterContext.Result = new RedirectResult("/Security/Index"); 

            //filterContext.Controller.ViewBag
        }
    }
}
