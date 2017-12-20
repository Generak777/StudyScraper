using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyScraper.Models.Domain
{
    public class ForgotPassword
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsValid { get; set; }
        public DateTime Expiration { get; set; }
    }
}
