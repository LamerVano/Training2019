using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using MyLogger;

namespace InfoPortal.Flters
{
    public class SQLExeptionAtribute : Attribute, IExceptionFilter
    {
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception;
            if (exception != null &&
                    exception is SqlException)
            {
                Log.Error(exception.Message + actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.NotAcceptable, exception.Message);
            }
            return Task.FromResult<object>(null);
        }
        public bool AllowMultiple
        {
            get { return true; }
        }
    }
}