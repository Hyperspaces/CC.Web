using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Dto.Articles
{
    public class ArticleDto
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 所属用户主键
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int Support { get; set; }

        /// <summary>
        /// 踩踏数
        /// </summary>
        public int Trample { get; set; }
    }
}
