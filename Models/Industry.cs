using System.ComponentModel.DataAnnotations;

namespace Rasttad.Models
{
    public class Industry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Slug { get; set; } = string.Empty; // مثل "it", "manufacturing"

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // "فناوری اطلاعات"

        [StringLength(500)]
        public string? Description { get; set; } // توضیح کوتاه

        [StringLength(20)]
        public string? ColorTheme { get; set; } // مثل "#004a99"

        [StringLength(200)]
        public string? HeroImage { get; set; } // مسیر تصویر

        // ارتباط با جداول دیگر
        public List<Challenge> Challenges { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<CaseStudy> CaseStudies { get; set; } = new();
    }
}