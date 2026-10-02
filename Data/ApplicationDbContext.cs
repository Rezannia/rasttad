using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rasttad.Models;

namespace Rasttad.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Industry> Industries { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<CaseStudy> CaseStudies { get; set; }
        public DbSet<Lead> Leads { get; set; }
    }
}