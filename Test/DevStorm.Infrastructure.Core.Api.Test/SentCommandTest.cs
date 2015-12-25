using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DevStorm.Application.Commands;
using DevStorm.Application.ReadModels;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.IOC;
using DevStorm.Infrastructure.DataAccess;
using DevStorm.Infrastructure.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DevStorm.Infrastructure.Core.Api.Test
{
    [TestClass]
    public class SentCommandTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            AssemblyService.LoadAllAssemblies();
            var cmd = new CreateEmployeeCommand {FirstName = "amin", LastName = "sheikhi"};

            DependencyManager.Resolve<ICommandBus>().Send(cmd);
        }

        [TestMethod]
        public void TestReadStorage()
        {
            var readStorage = new ReadStorage(new ReadContext());

            readStorage.GetById<EmployeeView>(Guid.Empty);
        }
    }
}
