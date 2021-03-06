﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Dto.System
{
   public class UserDto
   {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
   }
}
