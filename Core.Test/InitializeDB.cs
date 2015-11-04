using Core.DataAccess;
using Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Test
{
    [TestClass]
    public class InitializeDB
    {
        [TestMethod]
        public void TestEnumMetadata()
        {
            var context = new CoreDbContext();

            //context.Database.CreateIfNotExists();

            EnumMetadata.Seed(context);

            context.SaveChanges();
        }
    }
}
