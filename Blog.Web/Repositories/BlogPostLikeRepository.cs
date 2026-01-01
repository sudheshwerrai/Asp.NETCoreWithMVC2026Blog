using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Linq;

namespace Blog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogDbContext _dbContext = null;
        public BlogPostLikeRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _dbContext.BlogPostLike.AddAsync(blogPostLike);
            await _dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await _dbContext.BlogPostLike.CountAsync(bpl => bpl.BlogPostId == blogPostId);
        }

        public async Task<bool> IsUserLikedThisBlog(Guid userId,Guid blogPostId)
        {
            return await _dbContext.BlogPostLike
        .AnyAsync(bpl => bpl.UserId == userId && bpl.BlogPostId == blogPostId);
        }
             
    }
}
