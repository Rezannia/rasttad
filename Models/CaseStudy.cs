using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rasttad.Models
{
    public class CaseStudy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? Result { get; set; } // نتیجه (مثل "کاهش ۳۰٪ ترک خدمت")

        public int IndustryId { get; set; }

        [ForeignKey("IndustryId")]
        public Industry? Industry { get; set; }
    }
}