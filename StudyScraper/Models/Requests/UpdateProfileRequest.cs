using System.ComponentModel.DataAnnotations;

namespace StudyScraper.Models.Requests
{
    public class UpdateProfileRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(1)]
        public string MiddleInitial { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}
