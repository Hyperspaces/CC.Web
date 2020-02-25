using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC.Web.Dto.System;
using CC.Web.Service.System;
using Microsoft.AspNetCore.Mvc;

namespace CC.Web.Api.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Index([FromBody]UserDto user)
        {
            return Created(_userService.Add(user).ToString(), user);
        }
    }
}