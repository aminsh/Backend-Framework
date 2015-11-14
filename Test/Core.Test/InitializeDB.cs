using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Core.DataAccess;
using Core.Domain;
using Core.Domain.Contract;
using Core.Query;
using DataAccess;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadModels;

namespace Core.Test
{
    [TestClass]
    public class InitializeDB
    {
        [TestMethod]
        public void TestEnumMetadata()
        {
            var context = new AppDbContext();

            context.Database.CreateIfNotExists();
            //EnumMetadata.Seed(context);
            //context.SaveChanges();
            //var users = context.Set<User>().ToList();
        }

        [TestMethod]
        public void TestEmployeeAdd()
        {
            var context = new AppDbContext();

            var emp = new Employee
            {
                Id=Guid.NewGuid(),
                Code = "001",
                FirstName = "ali",
                LastName = "hassanzade"
            };

            context.Set<Employee>().Add(emp);

            context.SaveChanges();
        }

        [TestMethod]
        public void TestRemoveEmployeeByIRemovable()
        {
            var context = new AppDbContext();

            var emp = context.Set<Employee>().First();

            emp.Remove();

            context.SaveChanges();
        }

        [TestMethod]
        public void TestEmployee_ViewModel_Read()
        {
            var readStorage = new ReadStorage(new ReadContext());

            var empsView = readStorage.Get<EmployeeView>()
                .Where(emp=> emp.Name.Contains("ali"))
                .ToList();
        }
    }
}
