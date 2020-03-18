using CC.Web.Dao.Core;
using CC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.Web.Dao.Articles
{
    public class ArticleDao : BaseDao<Article>, IArticleDao
    {
        public List<Article> FindByUser(Guid userId)
        {
            return Repository.TableNoTracking
                .Where(e => !e.Deleted && e.UserId == userId)
                .ToList();
        }
    }


}
