using System.ComponentModel.DataAnnotations;

namespace P013KatmanliBlog.Core.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Soyadı")]
        public string? Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        public string? Password { get; set; }
        [Display(Name = "Telefon"), Phone]
        public string? Phone { get; set; }
        [Display(Name = "Oluşturulma Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Bloglar")]
        public List<Post>? Posts { get; set; }
    }
}
