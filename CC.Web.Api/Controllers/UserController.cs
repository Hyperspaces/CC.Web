using CC.Web.Dto.System;
using CC.Web.Service.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CC.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = HttpContext.User;
            return Ok("ok");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostUser([FromBody]UserAddDto user)
        {
            var userDto = _userService.Add(user);
            return Created(userDto.Id.ToString(), user);
        }

        [HttpGet("token/{userName}/{password}")]
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