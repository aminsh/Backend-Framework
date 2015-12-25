using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using DevStorm.Infrastructure.Core.Domain;
using DevStorm.Infrastructure.Core.Security;
using DevStorm.Infrastructure.Utility;

namespace DevStorm.Infrastructure.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EnumMetadata> EnumMetadatas { get; set; }

        static AppDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, MigrationManager<AppDbContext>>());
        }

        public AppDbContext()
            : base("dbConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetConfiguration(modelBuilder);

            LoadEntities(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            IgnoreRemovableTypes(modelBuilder);

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

        private static void LoadEntities(DbModelBuilder modelBuilder)
        {
            var entityTypes = TypeService.GetTypes(
                type => type.GetInterfaces().Any(its => its == typeof (IEntity)));

            var entityMethod = typeof (DbModelBuilder).GetMethod("Entity");

            entityTypes.ForEach(type =>
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] {}));
        }

        private static void SetConfiguration(DbModelBuilder modelBuilder)
        {
            var entityTypes = TypeService.GetTypes(type =>
                type.BaseType != null &&
                type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof (EntityTypeConfiguration<>));

            entityTypes.ForEach(type =>
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            });
        }

        public static void Ignore<TEntity>(DbModelBuilder modelBuilder) where TEntity : class, IRemovable
        {
            modelBuilder.Entity<TEntity>().Ignore(e => e.IsDeleted);
        }

        private static void IgnoreRemovableTypes(DbModelBuilder modelBuilder)
        {
            var entityTypes = TypeService.GetTypes(
                type => type.GetInterfaces().Any(its => its == typeof (IRemovable)));
            entityTypes.ForEach(type =>
            {
                var method = typeof (AppDbContext).GetMethod("Ignore", BindingFlags.Public | BindingFlags.Static)
                    .MakeGenericMethod(type);
                method.Invoke(null, new[] {modelBuilder});
            });
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

        protected override void Seed(TDbContext context)
        {
            EnumMetadata.Seed(context);

            base.Seed(context);
        }
    }
}