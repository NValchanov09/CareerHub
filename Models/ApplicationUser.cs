using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CareerHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Description { get; set; }

        public string? LinkedInURL { get; set; }

        public string? GitHubURL { get; set; }

        public string? ContactEmail { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;

        [ForeignKey("Company")]
        public int? CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}
