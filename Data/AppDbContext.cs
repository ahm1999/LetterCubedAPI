using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LettercubedApi.Models;
using Microsoft.Extensions.Hosting;

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

        //public DbSet<Actor> Actors { get; set; }
         public DbSet<Category> Categories { get; set; }
         public DbSet<MovieCategory> MovieCategories { get; set; }
         
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<WatchListItem> WatchListItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieCategory>().HasKey(vf => new { vf.MovieId, vf.CategoryId });
        }

    }
}
