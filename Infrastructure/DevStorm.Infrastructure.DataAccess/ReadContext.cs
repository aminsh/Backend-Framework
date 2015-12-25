using System;
using System.Data.Entity;
using System.Linq;
using DevStorm.Infrastructure.Core;
using DevStorm.Infrastructure.Utility;

namespace DevStorm.Infrastructure.DataAccess
{
    public class ReadContext : DbContext
    {
        public ReadContext()
            : base("name=dbConnection")
        {
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            LoadViewModels(modelBuilder);
        }

        private static void LoadViewModels(DbModelBuilder modelBuilder)
        {
            var entityTypes = TypeService.GetTypes(e => e.IsClass, AssemblyNameList.ReadModels);

            var entityMethod = typeof (DbModelBuilder).GetMethod("Entity");

            entityTypes.ForEach(type =>
            {
                dynamic entityConfiguratin = entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] {});
                entityConfiguratin.ToTable(type.Name);
            });
        }
    }
}