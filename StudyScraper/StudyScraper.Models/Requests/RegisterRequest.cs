using System.ComponentModel.DataAnnotations;

namespace StudyScraper.Models.Requests
{
    public class RegisterRequest
    {
        [Required, MaxLength(155)]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
    }
}
