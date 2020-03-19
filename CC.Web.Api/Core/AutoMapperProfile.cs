using AutoMapper;
using CC.Web.Dto.Articles;
using CC.Web.Dto.System;
using CC.Web.Model;
using CC.Web.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Web.Api.Core
{
    public class AutoMapperProfile
    {
        private MapperConfiguration _configuration;

        public AutoMapperProfile()
        {
            var config = new MapperConfiguration(cfg => 
            {
                //用户
                cfg.CreateMap<UserAddDto, User>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserUpdateDto, User>();
                cfg.CreateMap<User,UserUpdateDto>();

                //文章
                cfg.CreateMap<ArticleAddDto, Article>();
                cfg.CreateMap<Article, ArticleDto>();
            });

            _configuration = config;
        }

        public IMapper CreateMapper()
        {
            return _configuration.CreateMapper();
        }
    }
}
