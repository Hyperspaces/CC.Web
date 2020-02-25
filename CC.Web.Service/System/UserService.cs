using CC.Web.Dao.System;
using CC.Web.Dto.System;
using CC.Web.Model.System;
using CC.Web.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Service.System
{
    public class UserService : BaseService<User>, IUserService 
    {
        public IUserDao UserDao { get; set; }

        public Guid Add(UserDto userDto) 
        {
            var entity = new User()
            {
                NickName = userDto.NickName,
                UserName = userDto.UserName,
                PassWord = userDto.PassWord,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            return UserDao.Insert(entity);
        }

    }
}
