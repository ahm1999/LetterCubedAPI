using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LettercubedApi.Models;

namespace LettercubedApi.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
       base(options)
        { }

        public DbSet<Movie> Movies {get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        public DbSet<PromotionsLogs> PromotionsLogs { get; set; }
    }
}
