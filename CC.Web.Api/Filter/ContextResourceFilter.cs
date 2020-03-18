using CC.Web.Model.Core;
using CC.Web.Service.System;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Web.Api.Filter
{
    public class ContextResourceFilter : IAuthorizationFilter
    {
        private IWorkContext _workContext;
        private IUserService _userService;

        public ContextResourceFilter(IWorkContext workContext
            ,IUserService userService)
        {
            _workContext = workContext;
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaims = context.HttpContext.User.FindFirst("UserId");
            if (userIdClaims != null) 
            {
                var user = _userService.Find(Guid.Parse(userIdClaims.Value));
                _workContext.CurUser = user;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            return;


        }
    }
}
