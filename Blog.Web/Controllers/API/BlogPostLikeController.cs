using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository = null;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var blogPostLike = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId,
            };

            var blogPostLikeFromDb = await _blogPostLikeRepository.AddLikeForBlog(blogPostLike);
            return Ok(blogPostLikeFromDb);
        }

        [HttpGet("{blogPostId:Guid}")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] Guid blogPostId)
        {
            int totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPostId);
            return Ok(totalLikes);
        }

        [HttpGet("{userId:Guid}/{blogPostId:Guid}")]
        public async Task<bool> IsUserLikedBlog(Guid userId, Guid blogPostId)
        {
            return await _blogPostLikeRepository.IsUserLikedThisBlog(userId, blogPostId);
        }
    }
}
