using System.ComponentModel.DataAnnotations;

namespace P013KatmanliBlog.Core.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Bloglar")]
        public List<Post>? Posts { get; set; }
    }
}
