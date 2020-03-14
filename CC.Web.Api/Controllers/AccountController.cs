using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CC.Web.Api.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult<string> AccessDenied(string returnUrl)
        {
            return "Access Denied";
        }
    }
}