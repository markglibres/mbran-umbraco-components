using System;
using System.Collections.Generic;
using System.Linq;
using MBran.Components.Attributes;
using MBran.Components.Controllers;
using MBran.Components.Extensions;
using MBran.Components.Models;

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