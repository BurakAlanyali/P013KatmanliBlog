using Microsoft.EntityFrameworkCore;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Data.Concrete
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetAllByIncludeCategoryAndUserAsync()
        {
            return await _context.Posts.Include(x=> x.Category).Include(x=> x.User).ToListAsync();
        }

        public async Task<Post> GetByIdByIncludeCategoryAndUserAsync(int id)
        {
            return await _context.Posts.Include(x => x.Category).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Post>> GetSomeByIncludeCategoryAndUserAsync(Expression<Func<Post, bool>> predicate)
        {
            return await _context.Posts.Where(predicate).Include(x => x.Category).Include(x => x.User).ToListAsync();
        }
    }
}
