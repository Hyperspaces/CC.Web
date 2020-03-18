using CC.Web.Dto.Articles;
using CC.Web.Model;
using CC.Web.Service.Articles;
using CC.Web.Service.System;
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
        private readonly IUserService _userService;

        public ArticleController(IArticleService artcileSerice,
            IUserService userService)
        {
            _artcileSerice = artcileSerice;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult PostAddArtcile(ArticleAddDto addArticleDto) 
        {
            var articleDto = _artcileSerice.AddArtcile(addArticleDto);
            return Ok(articleDto);
        }

        [HttpGet("[userId]")]
        public IActionResult GetArtciles(Guid userId) 
        {
            var user = _userService.Find(userId);
            if (user == null)
                return BadRequest("未找到用户");

            return Ok(_artcileSerice.GetArtciles(userId));
        }
    }
}
