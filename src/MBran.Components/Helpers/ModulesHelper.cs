using System;
using System.Collections.Generic;
using System.Linq;
using MBran.Components.Attributes;
using MBran.Components.Controllers;
using MBran.Components.Extensions;
using MBran.Components.Models;
using Umbraco.Core;

namespace MBran.Components.Helpers
{
    public class ModulesHelper
    {
        private ModulesHelper()
        {
        }

        private static Lazy<ModulesHelper> _helper => new Lazy<ModulesHelper>(() => new ModulesHelper());
        public static ModulesHelper Instance => _helper.Value;

        public IEnumerable<ModuleDefinition> GetAll()
        {
            var cacheName = string.Join("_", GetType().FullName, nameof(GetAll), nameof(ModuleDefinition));

            return (IEnumerable<ModuleDefinition>) ApplicationContext.Current
                .ApplicationCache
                .RuntimeCache
                .GetCacheItem(cacheName, GetAllModules);
        }

        public Type GetModuleType(string typeFullName)
        {
            return AppDomain.CurrentDomain
                .FindImplementations<ModulesController>(typeFullName)
                .FirstOrDefault();
        }

        public IEnumerable<ModuleDefinition> GetDefinitions(IEnumerable<string> modules)
        {
            if (!modules?.Any() ?? true) return new List<ModuleDefinition>();

            return GetAll()
                .Where(module => modules.Contains(module.Value, StringComparer.InvariantCultureIgnoreCase));
        }

        private static IEnumerable<ModuleDefinition> GetAllModules()
        {
            var modules = AppDomain.CurrentDomain.FindImplementations<ModulesController>();
            if (modules == null || !modules.Any())
                return new List<ModuleDefinition>();

            return modules
                .Where(module => module != typeof(ModulesController))
                .Select(module =>
                {
                    var moduleAttribute = module.GetCustomAttributes(typeof(UmbracoModuleAttribute),
                        false).FirstOrDefault() as UmbracoModuleAttribute;

                    return new ModuleDefinition
                    {
                        Value = module.FullName,
                        Name = moduleAttribute?.Name ?? module.Name.Replace("Controller", string.Empty),
                        Description = moduleAttribute?.Description ?? string.Empty
                    };
                });
        }
    }
}