using AutoMapper;
using CC.Web.Dao.Articles;
using CC.Web.Dto.Articles;
using CC.Web.Model;
using CC.Web.Model.Core;
using CC.Web.Service.Core;
using CC.Web.Service.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.Web.Service.Articles
{
    public class ArticleService : BaseService<Article>, IArticleService
    {
        public IMapper Mapper { get; set; }

        public IUserService UserService { get; set; }

        public IArticleDao ArticleDao { get; set; }

        public ArticleDto AddArtcile(ArticleAddDto addArticleDto)
        {
            var article = Mapper.Map<Article>(addArticleDto);
            article.UserId = WorkContext.CurUser.Id;

            CurDao.Insert(article);

            return Mapper.Map<ArticleDto>(article);
        }

        public List<ArticleDto> GetArtciles(Guid userId)
        {
            var articles = ArticleDao.FindByUser(userId);
            return Mapper.Map<List<Article>, List<ArticleDto>>(articles);
        }
    }
}
