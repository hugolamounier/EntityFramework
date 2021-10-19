using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Adapter
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity => { 
                entity.HasOne(x => x.Author).WithMany(x => x.Books);
                entity.Navigation(x => x.Author).IsRequired();
            });
        }
    }
}
