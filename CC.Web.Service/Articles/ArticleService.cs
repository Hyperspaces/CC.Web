using CC.Web.Dto.Articles;
using CC.Web.Model;
using CC.Web.Model.Core;
using CC.Web.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Service.Articles
{
    public class ArticleService : BaseService<Article>, IArticleService
    {
        public ArticleDto AddArtcile(AddArticleDto addArticleDto)
        {
            var articleEntity = new Article()
            {
                Content = addArticleDto.Content,
                Title = addArticleDto.Title,
                UserId = WorkContext.CurUser.Id,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            CurDao.Insert(articleEntity);

            return new ArticleDto()
            {
                Id = articleEntity.Id,
                Title = articleEntity.Title,
                Content = articleEntity.Content,
                Support = articleEntity.Support,
                Trample = articleEntity.Trample
            };
        }
    }
}
