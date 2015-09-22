using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Core.Domain.Contract;
using Domain;
using Utility;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users{ get; set; }
        
       

        static AppDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, MigrationManager<AppDbContext>>());
        }

        public AppDbContext()
            :base("dbConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.Entries().Where(entry => entry.Entity is IRemovable)
               .Select(entry => entry.Entity.As<IRemovable>())
               .ToList()
               .ForEach(e =>
               {
                   if (e.IsDeleted)
                       Entry(e).State = EntityState.Deleted;
               });
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                return base.SaveChanges();
            } 
        }

        
    }

    
    public class MigrationManager<TDbContext> : DbMigrationsConfiguration<TDbContext>
    where TDbContext : DbContext
    {
        public MigrationManager()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}