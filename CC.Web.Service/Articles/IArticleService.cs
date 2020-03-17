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
        ArticleDto AddArtcile(AddArticleDto addArticleDto);
    }
}
