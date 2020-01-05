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
            //modelBuilder.Entity<Folder>()
            //    .HasMany(x => x.Items)
            //    .WithOne(x => x.Folder)
            //    .HasForeignKey(x => x.FolderId);
            ////modelBuilder.Entity<Item>()
            ////    .OwnsOne(c => c.Preview);
            //modelBuilder
            //.Entity<Item>()
            //.HasOne(u => u.Preview)
            //.WithOne(p => p.Item)
            //.HasForeignKey<Preview>(p => p.ItemId);
        }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<Document> Document { get; set; }
    }
}
