using Blog.Web.Data;
using Blog.Web.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext = null;
        public UserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            var users = await _authDbContext.Users.ToListAsync();
            var superAdminUser = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Email == "superadmin@blogapp.com");
            users.Remove(superAdminUser);
            return users;
        }       

        public async Task DeleteUser(Guid id)
        {
            var userFromDb = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
            if (userFromDb != null)
            {
                _authDbContext.Users.Remove(userFromDb);
                await _authDbContext.SaveChangesAsync();
            }
        }
    }
}
