using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Job> Jobs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
