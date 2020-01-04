using MediaArchieve.Shared;
using Microsoft.EntityFrameworkCore;

namespace MediaArchieve.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
      
        public DbSet<Folder> Folders{ get; set; }
    }
}
