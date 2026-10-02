using System.ComponentModel.DataAnnotations;

namespace Rasttad.Models
{
    public class Industry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(20)]
        public string? ColorTheme { get; set; }

        [StringLength(200)]
        public string? HeroImage { get; set; }

        // ==================== فیلدهای SEO ====================
        [StringLength(200)]
        public string? MetaTitle { get; set; }

        [StringLength(300)]
        public string? MetaDescription { get; set; }

        [StringLength(300)]
        public string? MetaKeywords { get; set; }

        // ==================== ارتباطات ====================
        public List<Challenge> Challenges { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<CaseStudy> CaseStudies { get; set; } = new();
    }
}