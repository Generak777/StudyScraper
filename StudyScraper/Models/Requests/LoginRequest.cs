using System.ComponentModel.DataAnnotations;

namespace StudyScraper.Models.Requests
{
    public class LoginRequest
    {
            public int Id { get; set; }
            [Required, MaxLength(155)]
            public string Email { get; set; }
            [Required, MaxLength(64)]
            public string Password { get; set; }
            public string EncryptedPass { get; set; }
            public string Salt { get; set; }
    }
}
