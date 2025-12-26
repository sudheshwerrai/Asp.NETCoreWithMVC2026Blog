using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _dbContext;
        public TagRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Tag tag)
        {
            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            _dbContext.Tags.Update(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tagFromDb=await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tagFromDb != null) 
            {
                _dbContext.Tags.Remove(tagFromDb);
                await _dbContext.SaveChangesAsync();    
            }
        }

    }
}
