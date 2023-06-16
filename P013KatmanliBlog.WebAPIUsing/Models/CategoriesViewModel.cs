using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Models
{
    public class CategoriesViewModel
    {
        public Post FeaturedPost { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
