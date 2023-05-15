using P013KatmanliBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Data.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetAllByIncludeCategoryAndUserAsync();
        Task<List<Post>> GetSomeByIncludeCategoryAndUserAsync(Expression<Func<Post, bool>> predicate);
        Task<Post> GetByIdByIncludeCategoryAndUserAsync(int id);
    }
}
