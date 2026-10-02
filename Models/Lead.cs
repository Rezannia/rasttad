using System.ComponentModel.DataAnnotations;

namespace Rasttad.Models
{
    public class Lead
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی الزامی است")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "شماره تماس الزامی است")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? IndustrySlug { get; set; }

        [StringLength(50)]
        public string? EmployeeCount { get; set; }

        [StringLength(50)]
        public string? Location { get; set; }

        [StringLength(1000)]
        public string? Message { get; set; }

        // فیلدهای جدید
        public bool IsFollowedUp { get; set; } = false;

        [StringLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}