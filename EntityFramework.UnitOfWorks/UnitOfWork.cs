using System.Collections.Generic;
using EntityFramework.Models;
using EntityFramework.UnitOfWorks.Interfaces;
using EntityFramework.UnitOfWorks.Seeds;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.UnitOfWorks
{
    public class UnitOfWork: DbContext, IUnitOfWork
    {
        #region Repositories
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        
        #endregion

        #region EntitiesConfiguration
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(AuthorSeed.Data);
            
            modelBuilder.Entity<Book>(entity => { 
                entity.HasOne(x => x.Author).WithMany(x => x.Books);
                entity.Navigation(x => x.Author).IsRequired();
            });
        }
        
        #endregion
        
        #region Transaction
        
        public void BeginTransaction()
        {
            Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Database.RollbackTransaction();
        }
        
        #endregion
    }
}