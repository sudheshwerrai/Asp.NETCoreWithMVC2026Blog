using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogDbContext _dbContext = null;
        public BlogPostCommentRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(BlogPostComment blogPostComment)
        {
            await _dbContext.BlogPostComment.AddAsync(blogPostComment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogPostIdAsync(Guid blogPostId)
        {
           return await _dbContext.BlogPostComment.Where(bpc => bpc.BlogPostId == blogPostId).ToListAsync();
        }

    }
}
