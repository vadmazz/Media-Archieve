using MediaArchieve.Shared;
using MediaArchieve.Shared.Items;
using Microsoft.EntityFrameworkCore;

namespace MediaArchieve.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<Clip> Clips { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
