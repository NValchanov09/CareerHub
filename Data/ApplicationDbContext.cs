using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CareerHub.Models;

namespace CareerHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CareerHub.Models.Language> Language { get; set; } = default!;
        public DbSet<CareerHub.Models.Company> Company { get; set; } = default!;
        public DbSet<CareerHub.Models.JobPosition> JobPosition { get; set; } = default!;
    }
}
