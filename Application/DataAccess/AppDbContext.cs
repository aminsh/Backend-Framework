using System.Data.Entity;
using Core.DataAccess;
using Domain;

namespace DataAccess
{
    public class AppDbContext : CoreDbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Define entity configurations here

            base.OnModelCreating(modelBuilder);
        }
    }
}
