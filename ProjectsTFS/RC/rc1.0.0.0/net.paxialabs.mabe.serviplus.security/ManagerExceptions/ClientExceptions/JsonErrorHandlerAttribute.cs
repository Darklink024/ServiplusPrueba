using System.Collections.Generic;
using System.Web.Mvc;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions.ClientExceptions
{
    public sealed class JsonErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int)HtmlStatusCode.Internal_Server_Error;
            filterContext.ExceptionHandled = true;

            try
            {
                Dictionary<string, string> dataException = ParseToMasterException.GetErroCodeAndErrorGUI(filterContext.Exception);

                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        server_Error_Code = dataException["ErroCode"].ToString(),
                        server_Error_GUI = dataException["ErrorGUI"].ToString(),
                        server_Error_Description = filterContext.Exception.Message,
                        serverError_Message = dataException["ErrorDescription"].ToString(),
                        serverError_stackTrace = filterContext.Exception.StackTrace
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
            catch
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        server_Error_Code = "N/A",
                        server_Error_Description = filterContext.Exception.Message,
                        serverError_Message = filterContext.Exception.Message,
                        serverError_stackTrace = filterContext.Exception.StackTrace
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}
