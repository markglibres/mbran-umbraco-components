using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;

namespace MBran.Components.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> FindImplementations<T>(this AppDomain domain)
            where T : class
        {
            return (IEnumerable<Type>)ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem(
                string.Join("_", new[] { "MBran.Components.Extensions.AssemblyExtensions.FindImplementations", typeof(T).FullName}),
                () => GetImplementations(domain, typeof(T)));
        }

        public static Type FindImplementation(this AppDomain domain, string objectFullName)
        {
            return (Type)ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem(
                string.Join("_", new[] { "MBran.Components.Extensions.AssemblyExtensions.FindImplementation", objectFullName }),
                () => GetImplementation(domain, objectFullName));
        }

        public static IEnumerable<Type> FindImplementations(this AppDomain domain, string typeName)
        {
            return (IEnumerable<Type>)ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem(
                string.Join("_", new[] { "MBran.Components.Extensions.AssemblyExtensions.FindImplementations.TypeName", typeName }),
                () => GetImplementationByName(domain, typeName));
        }

        internal static IEnumerable<Type> GetImplementations(AppDomain domain, Type findType)
        {
            return domain.GetAssemblies()
                .Where(assembly => !assembly.GlobalAssemblyCache)
                .SelectMany(assembly => assembly.GetTypes())
                .Where(findType.IsAssignableFrom);
        }

        internal static Type GetImplementation(AppDomain domain, string objectFullName)
        {
            return domain
                .GetAssemblies()
                .Where(assembly => !assembly.GlobalAssemblyCache)
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type =>
                    type.FullName.Equals(objectFullName, StringComparison.InvariantCultureIgnoreCase)
                    || type.AssemblyQualifiedName.Equals(objectFullName, StringComparison.InvariantCultureIgnoreCase));
        }

        internal static IEnumerable<Type> GetImplementationByName(AppDomain domain, string name)
        {
            return domain
                .GetAssemblies()
                .Where(assembly => !assembly.GlobalAssemblyCache)
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                    type.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

    }
}
