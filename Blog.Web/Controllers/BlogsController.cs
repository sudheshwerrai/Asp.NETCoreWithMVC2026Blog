using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{    
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository = null;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository = null;
        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpGet]
        [ActionName("BlogPostDetail")]
        public async Task<IActionResult> Index(string urlHandler)
        {
            var blogPost = await _blogPostRepository.GetBlogByUrlHandler(urlHandler);
            BlogDetailsViewModel blogPostViewModel = null;
            if (blogPost != null) 
            {
                int totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPost.Id);
                blogPostViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                };

            }

            return View("BlogPostDetail", blogPostViewModel);
        }
    }
}
