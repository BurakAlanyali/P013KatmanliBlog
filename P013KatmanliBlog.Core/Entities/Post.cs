using System.ComponentModel.DataAnnotations;

namespace P013KatmanliBlog.Core.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "İçerik"), DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [Display(Name = "Oluşturulma Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }
        [Display(Name = "Yazar")]
        public int UserId { get; set; }
        [Display(Name = "Yazar")]
        public User? User { get; set; }
    }
}
