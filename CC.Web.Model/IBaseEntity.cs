using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Model
{
    public interface IEntity
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        bool Deleted { get; set; }

        /// <summary>
        /// 数据行同步标记
        /// </summary>
        int RowVersion { get; set; }
    }
}
