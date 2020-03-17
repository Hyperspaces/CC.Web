using CC.Web.Dto.Articles;
using CC.Web.Model;
using CC.Web.Service.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ArticleController : Controller
    {
        private IArticleService _artcileSerice;

        public ArticleController(IArticleService artcileSerice)
        {
            _artcileSerice = artcileSerice;
        }

        public IActionResult PostAddArtcile(AddArticleDto addArticleDto) 
        {
            var user = HttpContext.User;

            _artcileSerice.AddArtcile(addArticleDto);
            return Ok();
        }
    }
}
