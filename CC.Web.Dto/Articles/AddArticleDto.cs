using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Dto.Articles
{
    public class AddArticleDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
