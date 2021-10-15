using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Adapter
{
    public class MySQLDatabaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public MySQLDatabaseContext(DbContextOptions<MySQLDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(m => m.Title).HasMaxLength(255);
            });
        }
    }
}
