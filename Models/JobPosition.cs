using Microsoft.EntityFrameworkCore.Design.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CareerHub.Models
{
    public class JobPosition
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Type { get; set; }

        public string? Level { get; set; }

        public int? YearsFrom { get; set; }

        public int? YearsTo { get; set; }

        public bool? IsFullTime { get; set; }

        public bool? IsRemote { get; set; }

        public decimal? SalaryFrom { get; set; }

        public decimal? SalaryTo { get; set; }

        public ICollection<Language> RequiredLanguages { get; set; } = new List<Language>();

        public string? RecruiterId { get; set; }

        [ForeignKey("RecruiterId")]
        public ApplicationUser? Recruiter { get; set; }
        
        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        public DateTime PostedAt { get; set; } = DateTime.Now;
    }
}
