using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rasttad.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Icon { get; set; } // نام آیکون

        public int IndustryId { get; set; }

        [ForeignKey("IndustryId")]
        public Industry? Industry { get; set; }
    }
}