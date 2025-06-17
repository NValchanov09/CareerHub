using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CareerHub.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? WebsiteURL { get; set; }

        public string? LinkedInURL { get; set; }

        public string? ContactEmail { get; set; }

        public string? LogoPath { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<ApplicationUser> Recruiters { get; set; } = new List<ApplicationUser>();

        public ICollection<JobPosition> JobPositions { get; set; } = new List<JobPosition>();
    }
}
