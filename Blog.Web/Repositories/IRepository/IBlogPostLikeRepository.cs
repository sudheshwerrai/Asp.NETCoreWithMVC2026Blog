using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories.IRepository
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
        Task<bool> IsUserLikedThisBlog(Guid userId, Guid blogPostId);
    }
}
