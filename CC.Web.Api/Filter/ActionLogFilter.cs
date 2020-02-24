using CC.Web.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Web.Api.Filter
{
    public class ActionLogFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var path = context.HttpContext.Request.Path;
            LogHelper.DebugLog($"RequestPath={path}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
