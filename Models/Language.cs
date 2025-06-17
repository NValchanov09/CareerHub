using System.ComponentModel.DataAnnotations;

namespace CareerHub.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? LogoPath { get; set; }

        public ICollection<JobPosition> JobPositions { get; set; } = new List<JobPosition>();
    }
}
