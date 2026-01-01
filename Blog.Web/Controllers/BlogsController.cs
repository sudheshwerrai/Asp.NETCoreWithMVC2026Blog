using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository = null;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository = null;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository = null;
        private readonly UserManager<IdentityUser> _userManager = null;
        private readonly SignInManager<IdentityUser> _signInManager = null;

        public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            IBlogPostCommentRepository blogPostCommentRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _blogPostCommentRepository = blogPostCommentRepository;
            _userManager = userManager;
            _signInManager = signInManager;
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

                var blogPostCommentsFromDb=await _blogPostCommentRepository.GetCommentsByBlogPostIdAsync(blogPost.Id);

                var blogPostComments = new List<BlogCommentViewModel>();

                foreach (var comment in blogPostCommentsFromDb)
                {
                    var postComment = new BlogCommentViewModel
                    {
                        UserName = (await _userManager.FindByIdAsync(comment.UserId.ToString())).UserName,
                        DateAdded = comment.DateAdded,
                        Description=comment.Description
                    };
                    blogPostComments.Add(postComment);
                }
                

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
                    Comments= blogPostComments
                };

            }

            return View("BlogPostDetail", blogPostViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBlogComment(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var blogPostComment = new BlogPostComment
                {
                    Description = blogDetailsViewModel.CommentDescription,
                    BlogPostId = blogDetailsViewModel.Id,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };
                await _blogPostCommentRepository.AddAsync(blogPostComment);
                return RedirectToAction("BlogPostDetail", "Blogs", new { urlHandler = blogDetailsViewModel.UrlHandle });
            }
            return View();

        }
    }
}
