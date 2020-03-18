using CC.Web.Dao.Core;
using CC.Web.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Dao.Articles
{
    public interface IArticleDao : IBaseDao<Article>
    {
        List<Article> FindByUser(Guid userId);
    }
}
