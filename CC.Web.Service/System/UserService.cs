using AutoMapper;
using CC.Web.Dao.System;
using CC.Web.Dto.System;
using CC.Web.Model.System;
using CC.Web.Service.Core;
using CC.Web.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CC.Web.Service.System
{
    public class UserService : BaseService<User>, IUserService 
    {
        public IUserDao UserDao { get; set; }

        public IConfiguration Configuration { get; set; }

        public IMapper Mapper { get; set; }

        public UserDto Add(UserAddDto addUserDto) 
        {
            if(UserDao.FindUserByName(addUserDto.UserName) != null)
            {
                throw new ArgumentException("用户名重复");
            }

            var encryptPwd = EncryptHelper.ComputeHash(addUserDto.PassWord);

            var entity = Mapper.Map<User>(addUserDto);
            entity.PassWord = encryptPwd;

            return Mapper.Map<UserDto>(entity);
        }

        public User FindUserByNameAndPwd(string userName, string password)
        {
            var encryptPwd = EncryptHelper.ComputeHash(password);
            var user = UserDao.FindUserByNameAndPwd(userName, encryptPwd);
            if (user == null)
                throw new UnauthorizedAccessException("登录的用户不存在");
            return user;
        }

        public string RequestToken(User user)
        {
            // push the user’s name into a claim, so we can identify the user later on.
            var claims = new[]
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.Gender, "1111"),
                   new Claim(ClaimTypes.Uri, "222"),
                   new Claim("UserId", user.Id.ToString()),
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var issuer = Configuration["Auth:JwtIssuer"];
            var expire = Configuration["Auth:JwtExpireMinutes"];

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(expire)),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
