using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Dto.System
{
    public class UserLoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
