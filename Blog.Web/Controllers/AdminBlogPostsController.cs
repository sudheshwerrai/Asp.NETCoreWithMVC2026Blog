using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository _tagRepository = null;
        private readonly IBlogPostRepository _blogPostRepository = null;
        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }

        [HttpGet]
        public async Task<ViewResult> Create()
        {
            var tags = await _tagRepository.GetAllAsync();
            var blogPost = new BlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),

            };
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostRequest blogPostRequest)
        {
            if (ModelState.IsValid)
            {
                var blogPost = new BlogPost
                {
                    Heading = blogPostRequest.BlogPost.Heading,
                    PageTitle = blogPostRequest.BlogPost.PageTitle,
                    Content = blogPostRequest.BlogPost.Content,
                    ShortDescription = blogPostRequest.BlogPost.ShortDescription,
                    FeaturedImageUrl = blogPostRequest.BlogPost.FeaturedImageUrl,
                    UrlHandle = blogPostRequest.BlogPost.UrlHandle,
                    PublishedDate = blogPostRequest.BlogPost.PublishedDate,
                    Author = blogPostRequest.BlogPost.Author,
                    Visible = blogPostRequest.BlogPost.Visible,
                };

                var listOfTags = new List<Tag>();
                foreach (var selectedTagId in blogPostRequest.SelectedTags)
                {
                    var selectedTag = Guid.Parse(selectedTagId);
                    var existingTag = await _tagRepository.GetAsync(selectedTag);
                    if (existingTag != null)
                    {
                        listOfTags.Add(existingTag);
                    }
                }

                blogPost.Tags = listOfTags;
                await _blogPostRepository.AddAsync(blogPost);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ViewResult> Edit(Guid id)
        {
            var tags = await _tagRepository.GetAllAsync();

            var blogPostFromDb = await _blogPostRepository.GetAsync(id);
            
            var blogPost = new BlogPostRequest();            
            blogPost.Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            blogPost.SelectedTags = blogPostFromDb.Tags.Select(x => x.Id.ToString()).ToArray();
            blogPost.BlogPost = blogPostFromDb;
            
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BlogPostRequest blogPostRequest)
        {
            if (ModelState.IsValid && blogPostRequest.BlogPost.Id == id)
            {
                var blogPost = new BlogPost
                {
                    Id= blogPostRequest.BlogPost.Id,
                    Heading = blogPostRequest.BlogPost.Heading,
                    PageTitle = blogPostRequest.BlogPost.PageTitle,
                    Content = blogPostRequest.BlogPost.Content,
                    ShortDescription = blogPostRequest.BlogPost.ShortDescription,
                    FeaturedImageUrl = blogPostRequest.BlogPost.FeaturedImageUrl,
                    UrlHandle = blogPostRequest.BlogPost.UrlHandle,
                    PublishedDate = blogPostRequest.BlogPost.PublishedDate,
                    Author = blogPostRequest.BlogPost.Author,
                    Visible = blogPostRequest.BlogPost.Visible,
                };

                var listOfTags = new List<Tag>();
                foreach (var selectedTagId in blogPostRequest.SelectedTags)
                {
                    var selectedTag = Guid.Parse(selectedTagId);
                    var existingTag = await _tagRepository.GetAsync(selectedTag);
                    if (existingTag != null)
                    {
                        listOfTags.Add(existingTag);
                    }
                }

                blogPost.Tags = listOfTags;
                await _blogPostRepository.UpdateAsync(blogPost);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Delete(Guid id)
        {
            await _blogPostRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
