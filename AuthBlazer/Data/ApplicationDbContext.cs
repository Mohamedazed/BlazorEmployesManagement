using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthBlazer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee>? Employees { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<ApplicationUser>()
        //           .Ignore(u => u.Roles);
        //}
    }
}
