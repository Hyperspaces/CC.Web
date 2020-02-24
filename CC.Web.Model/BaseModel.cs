using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Model
{
    /// <summary>
    /// 基础公共类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime InsertTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 行记录
        /// </summary>
        public string RowVersion { get; set; }
    }
}
