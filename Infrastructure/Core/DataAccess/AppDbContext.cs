using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Configuration;
using Core.Domain;
using Core.Domain.Contract;
using Utility;

namespace Core.DataAccess
{
    public class AppDbContext : DbContext
    {
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
            AppDomain.CurrentDomain.Load(AssemblyNameList.Domain);

            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Single(assembly => assembly.GetName().Name == AssemblyNameList.Domain)
                .GetTypes()
                .Where(type => type.GetInterfaces().Any(its => its == typeof (IEntity)))
                .ToList();

            var entityMethod = typeof (DbModelBuilder).GetMethod("Entity");

            entityTypes.ForEach(type =>
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] {}));
        }

        private static void SetConfiguration(DbModelBuilder modelBuilder)
        {
            AppDomain.CurrentDomain.Load(AssemblyNameList.Persentation);

            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Single(assembly => assembly.GetName().Name == AssemblyNameList.Persentation)
                .GetTypes()
                .Where(type =>
                    type.BaseType != null &&
                    type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof (EntityTypeConfiguration<>))
                .ToList();

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
            AppDomain.CurrentDomain.Load(AssemblyNameList.Domain);

            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Single(assembly => assembly.GetName().Name == AssemblyNameList.Domain)
                .GetTypes()
                .Where(type => type.GetInterfaces().Any(its => its == typeof (IRemovable)))
                .ToList();

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