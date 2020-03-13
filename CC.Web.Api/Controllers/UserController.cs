using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC.Web.Dto.System;
using CC.Web.Service.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CC.Web.Api.Controllers
{
    [Authorize]
    [Route("api/user")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult PostUser([FromBody]UserDto user)
        {
            return Created(_userService.Add(user).ToString(), user);
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult Token(string userName, string password)
        {
            var user = _userService.FindUserByNameAndPwd(userName, password);
            if (user == null)
                return new UnauthorizedResult();
            return Ok(_userService.RequestToken(user));
        }
    }
}