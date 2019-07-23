using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MyLogger;
using Common.Exceptions;

namespace InfoPortal.Flters
{
    public class Logging : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Log.Debug("Start " + actionContext.Request.Method.Method + " " + actionContext.ControllerContext.ControllerDescriptor.ControllerName + " " + actionContext.ActionDescriptor.ActionName);
                
            });
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext,
                                        CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Log.Debug("End " + actionExecutedContext.ActionContext.Request.Method.Method +  " " + actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName + " " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            });
        }
    }
}