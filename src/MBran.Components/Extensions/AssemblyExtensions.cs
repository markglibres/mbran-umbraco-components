using System;
using System.Collections.Generic;
using System.Linq;

namespace MBran.Components.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> FindImplementations<T>(this AppDomain domain)
            where T : class
        {
            var findType = typeof(T);
            return domain.GetAssemblies()
                .Where(assembly => !assembly.GlobalAssemblyCache)
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => findType.IsAssignableFrom(type));
        }

        public static Type FindImplementation(this AppDomain domain, string objectFullName)
        {
            return domain
                .GetAssemblies()
                .Where(assembly => !assembly.GlobalAssemblyCache)
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => 
                    type.FullName.Equals(objectFullName,StringComparison.InvariantCultureIgnoreCase)
                    || type.AssemblyQualifiedName.Equals(objectFullName, StringComparison.InvariantCultureIgnoreCase));
        }

    }
}
