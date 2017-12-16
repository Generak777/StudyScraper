using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyScraper.Models.ViewModels
{
    public class SavedPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
