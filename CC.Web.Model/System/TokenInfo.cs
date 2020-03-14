using CC.Web.Model;
using System;

namespace Best.SFA.Domain.Security
{
    /// <summary>
    ///
    /// </summary>
    public class TokenInfo : BaseModel
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// token的状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid UserId { get; set; }
    }
}