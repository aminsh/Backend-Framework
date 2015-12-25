using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevStorm.Infrastructure.Utility
{
    public static class TypeService
    {
        public static IEnumerable<Type> GetTypes(Func<Type, bool> exp)
        {
            return AssemblyService.GetAssemblies()
                .SelectMany(e => e.GetTypes())
                .Where(exp).ToList();
        }

        public static IEnumerable<Type> GetTypes(Func<Type, bool> exp, string assemblyName)
        {
            return AssemblyService.GetAssembly(assemblyName).GetTypes().Where(exp);
        }

        public static Type GetType(Func<Type, bool> exp)
        {
            return AssemblyService.GetAssemblies()
                .SelectMany(e => e.GetTypes()).SingleOrDefault(exp);
        }
    }
}