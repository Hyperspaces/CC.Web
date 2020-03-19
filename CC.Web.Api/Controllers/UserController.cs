using AutoMapper;
using CC.Web.Dto.System;
using CC.Web.Model.System;
using CC.Web.Service.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CC.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet("{userId}",Name = nameof(GetUser))]
        public ActionResult<UserDto> GetUser(Guid userId)
        {
            var userDto = _userService.FindUserById(userId);

            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">添加用户Dto</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostUser([FromBody]UserAddDto user)
        {
            var returnDto = _userService.Add(user);

            return CreatedAtRoute(nameof(GetUser), new { userId = returnDto.Id }, returnDto);
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet("token/{userName}/{password}")]
        [AllowAnonymous]
        public IActionResult Token(string userName, string password)
        {
            var user = _userService.FindUserByNameAndPwd(userName, password);
            if (user == null)
                return NotFound();

            return Ok(_userService.RequestToken(user));
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateUser([FromRoute]Guid userId,[FromBody]UserUpdateDto updateUserDto) 
        {
            if (updateUserDto is null)
            {
                return BadRequest();
            }

            _userService.UpdateUser(userId, updateUserDto);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPatch("userId")]
        public ActionResult PartiallyUpdateUser(Guid userId,JsonPatchDocument<UserUpdateDto> patchDocument) 
        {
            var userEntity = _userService.Find(userId);

            var userUpdateDto = _mapper.Map<UserUpdateDto>(userEntity);
            patchDocument.ApplyTo(userUpdateDto);

            userEntity = _mapper.Map(userUpdateDto,userEntity);

            _userService.Update(userEntity);

            return Ok();
        }
    }
}