using System.ComponentModel.DataAnnotations;

namespace StudyScraper.Models.Requests
{
    public class UpdatePostRequest
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(300)]
        public string Title { get; set; }
        [Required, MaxLength(150)]
        public string Url { get; set; }
        [Required, MaxLength(500)]
        public string Notes { get; set; }
    }
}
