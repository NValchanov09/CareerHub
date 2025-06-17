using System.ComponentModel.DataAnnotations.Schema;

namespace CareerHub.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        public string? CandidateId { get; set; }

        [ForeignKey("CandidateId")]
        public ApplicationUser? Candidate { get; set; }

        public int? JobPositionId { get; set; }

        [ForeignKey("JobPositionId")]
        public JobPosition? JobPosition { get; set; }

        public string? CVPath { get; set; }

        public string? CoverLetterPath { get; set; }

        public DateTime? SubmitedAt { get; set; } = DateTime.Now;

        public JobApplicationStatus? Status { get; set; }
    }
}
