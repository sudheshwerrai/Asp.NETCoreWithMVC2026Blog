using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories.IRepository
{
    public interface IBlogPostCommentRepository
    {
        Task AddAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogPostIdAsync(Guid blogPostId);
    }
}
