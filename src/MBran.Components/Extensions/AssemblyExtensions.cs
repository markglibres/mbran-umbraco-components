using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;

namespace MBran.Components.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> FindImplementations<T>(this AppDomain domain, string typeFullName = "")
            where T : class
        {
            string cacheName = string.Join("_", new[] {
                    typeof(AssemblyExtensions).FullName,
                    nameof(FindImplementations),
                    typeof(T).FullName});

            return (IEnumerable<Type>)ApplicationContext.Current
                .ApplicationCache
                .RuntimeCache
                .GetCacheItem(cacheName, () => GetImplementations(domain, typeof(T), typeFullName));
        }
        
        public static Type FindImplementation(this AppDomain domain, string objectFullName)
        {
            string cacheName = string.Join("_", new[] {
                    typeof(AssemblyExtensions).FullName,
                    nameof(FindImplementation),
                    objectFullName});

            return (Type)ApplicationContext.Current
                .ApplicationCache
                .RuntimeCache
                .GetCacheItem(cacheName, () => GetImplementation(domain, objectFullName));
        }

        public static IEnumerable<Type> FindImplementations(this AppDomain domain, string typeName)
        {
            string cacheName = string.Join("_", new[] {
                    typeof(AssemblyExtensions).FullName,
                    nameof(FindImplementations),
                    nameof(Type),
                    typeName});

            return (IEnumerable<Type>)ApplicationContext.Current
                .ApplicationCache
                .RuntimeCache
                .GetCacheItem(cacheName, () => GetImplementationByName(domain, typeName));
        }

        internal static IEnumerable<Type> GetImplementations(AppDomain domain, Type findType, string typeFullName = "")
        {
            return domain.GetAssemblies()
                .Where(assembly => !assembly.GlobalAssemblyCache)
                .SelectMany(assembly => assembly.GetTypes())
                .Where(findType.IsAssignableFrom)
                .Where(type => string.IsNullOrWhiteSpace(typeFullName) || 
                    type.FullName.Equals(typeFullName,StringComparison.InvariantCultureIgnoreCase)
                );
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
