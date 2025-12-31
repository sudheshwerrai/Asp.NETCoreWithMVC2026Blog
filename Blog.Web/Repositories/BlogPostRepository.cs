using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _dbContext = null;

        public BlogPostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
          return await _dbContext.BlogPosts.Include(b=>b.Tags).ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
           return await _dbContext.BlogPosts.Include(bp=>bp.Tags).FirstOrDefaultAsync(bp => bp.Id == id);
        }

        public async Task<BlogPost> GetBlogByUrlHandler(string urlHandler)
        {
            return await _dbContext.BlogPosts.Include(bp => bp.Tags).FirstOrDefaultAsync(bp => bp.UrlHandle == urlHandler);
        }
       
        public async Task AddAsync(BlogPost blogPost)
        {
           await _dbContext.BlogPosts.AddAsync(blogPost);
           await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogPost blogPost)
        {                         
            var existingBlogPost = _dbContext.BlogPosts.Include(bp => bp.Tags).FirstOrDefault(bp => bp.Id == blogPost.Id);
            existingBlogPost.Id=blogPost.Id;
            existingBlogPost.Heading = blogPost.Heading;
            existingBlogPost.PageTitle = blogPost.PageTitle;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.ShortDescription = blogPost.ShortDescription;
            existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existingBlogPost.UrlHandle = blogPost.UrlHandle;
            existingBlogPost.PublishedDate = blogPost.PublishedDate;
            existingBlogPost.Author = blogPost.Author;
            existingBlogPost.Visible = blogPost.Visible;
            existingBlogPost.Tags= blogPost.Tags;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var blogFromDb = await _dbContext.BlogPosts.FirstOrDefaultAsync(t => t.Id == id);
            if (blogFromDb != null)
            {
                _dbContext.BlogPosts.Remove(blogFromDb);
                await _dbContext.SaveChangesAsync();
            }
        }     
    }
}
