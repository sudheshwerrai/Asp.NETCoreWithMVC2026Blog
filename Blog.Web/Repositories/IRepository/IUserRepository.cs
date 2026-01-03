using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();        
        Task DeleteUser(Guid id);
    }
}
