using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.MVCUI.Models
{
    public class HomePageViewModel
    {
        public Post FeaturedPost { get; set; }
        public List<Post> Posts { get; set;} 
    }
}
