using CC.Web.Dto.Articles;
using CC.Web.Model;
using CC.Web.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Service.Articles
{
    public interface IArticleService : IBaseService<Article>
    {
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="addArticleDto"></param>
        /// <returns></returns>
        ArticleDto AddArtcile(ArticleAddDto addArticleDto);

        /// <summary>
        /// 根据UserId获取文章
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ArticleDto> GetArtciles(Guid userId);
    }
}
