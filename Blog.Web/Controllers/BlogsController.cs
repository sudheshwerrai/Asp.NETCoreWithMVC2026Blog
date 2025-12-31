using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Authorize(Roles ="User")]
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository = null;
        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository=blogPostRepository;
        }

        [HttpGet]
        [ActionName("BlogPostDetail")]
        public async Task<IActionResult> Index(string urlHandler)
        {
            var blogPost = await _blogPostRepository.GetBlogByUrlHandler(urlHandler);            
            return View("BlogPostDetail", blogPost);
        }
    }
}
