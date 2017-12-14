using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyScraper.Models.ViewModels
{
    public class RedditPost
    {
        public List<string> PostTitle { get; set; }
        public List<string> PostUrl { get; set; }
        public List<string> StudyTitle { get; set; }
        public List<string> StudyUrl { get; set; }
    }
}
