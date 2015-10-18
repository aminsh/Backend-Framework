using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Core.Domain.Contract;
using Utility;

namespace Core.DataAccess
{
    public class CoreDbContext : DbContext
    {
        static CoreDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CoreDbContext, MigrationManager<CoreDbContext>>());
        }

        public CoreDbContext()
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