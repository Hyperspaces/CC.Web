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
        Guid Add(UserDto userDto);
    }
}
