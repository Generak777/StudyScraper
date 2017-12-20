using System.ComponentModel.DataAnnotations;

namespace StudyScraper.Models.Requests
{
    public class UpdatePasswordRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required, MinLength(6)]
        public string NewPassword { get; set; }
    }
}
