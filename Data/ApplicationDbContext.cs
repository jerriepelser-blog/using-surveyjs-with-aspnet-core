using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SurveyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CompletedSurvey> CompletedSurveys { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}