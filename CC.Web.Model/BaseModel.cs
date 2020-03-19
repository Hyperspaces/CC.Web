using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Web.Model
{
    /// <summary>
    /// 基础公共类
    /// </summary>
    public class BaseModel : IEntity
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// 插入时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime InsertTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 行记录
        /// </summary>
        public int RowVersion { get; set; }
    }
}
