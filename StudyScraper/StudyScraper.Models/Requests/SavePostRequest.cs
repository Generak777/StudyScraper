using System.ComponentModel.DataAnnotations;

namespace StudyScraper.Models.Requests
{
    public class SavePostRequest
    {
        [Required, MaxLength(300)]
        public string Title { get; set; }
        [Required, MaxLength(150)]
        public string Url { get; set; }
    }
}
