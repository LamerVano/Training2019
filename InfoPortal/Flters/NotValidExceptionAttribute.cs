using MyLogger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using Common.Exceptions;

namespace InfoPortal.Flters
{
    public class NotValidExceptionAttribute : Attribute, IExceptionFilter
    {
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception;
            if (exception != null &&
                    exception is NotValidException)
            {
                Log.Error(exception.Message + " in action: " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.NotModified, "Not corect data");
            }
            return Task.FromResult<object>(null);
        }
        public bool AllowMultiple
        {
            get { return true; }
        }
    }
}