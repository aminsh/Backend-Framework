using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Core.Query
{
    public class ReadContext : DbContext
    {
        public ReadContext()
            : base("name=dbConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            LoadViewModels(modelBuilder);
        }

        private static void LoadViewModels(DbModelBuilder modelBuilder)
        {
            AppDomain.CurrentDomain.Load("ReadModels");

            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Single(assembly => assembly.GetName().Name == "ReadModels")
                .GetTypes()
                .Where(type => type.IsClass)
                .ToList();

            var entityMethod = typeof (DbModelBuilder).GetMethod("Entity");

            entityTypes.ForEach(type =>
            {
                dynamic entityConfiguratin = entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] {});
                entityConfiguratin.ToTable(type.Name);
            });
        }
    }
}