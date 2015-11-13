using System.Data.Entity.ModelConfiguration;
using Domain;

namespace DataAccessConfigurations
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            Property(e => e.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
