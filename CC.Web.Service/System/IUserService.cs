using CC.Web.Dto.System;
using CC.Web.Model.System;
using CC.Web.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Service.System
{
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        UserDto Add(UserAddDto userDto);

        /// <summary>
        /// 通过用户名密码查询
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        User FindUserByNameAndPwd(string userName,string password);

        /// <summary>
        /// 指定的用户获得Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string RequestToken(User user);
    }
}
