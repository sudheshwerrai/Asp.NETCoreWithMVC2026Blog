using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories.IRepository
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync(bool visibleAll = true);
        Task<BlogPost> GetAsync(Guid id);
        Task<BlogPost> GetBlogByUrlHandler(string urlHandler);
        Task AddAsync(BlogPost blogPost);
        Task UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(Guid id);
    }
}
