using MBran.Components.Attributes;
using MBran.Components.Controllers;
using MBran.Components.Extensions;
using MBran.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBran.Components.Helpers
{
    public class ModulesHelper
    {
        private static Lazy<ModulesHelper> _helper => new Lazy<ModulesHelper>(() => new ModulesHelper());
        public static ModulesHelper Instance => _helper.Value;

        private ModulesHelper()
        {
        }

        public IEnumerable<ModuleDefinition> GetAll()
        {
            IEnumerable<Type> modules = AppDomain.CurrentDomain.FindImplementations<ModulesController>();
            if(modules == null || modules.Count() == 0)
            {
                return new List<ModuleDefinition>();
            }

            return modules
                .Where(module => !module.Equals(typeof(ModulesController)))
                .Select(module => new ModuleDefinition
                {
                    Value = module.FullName,
                    Name = (module.GetCustomAttributes(typeof(ModuleNameAttribute),
                        false).FirstOrDefault() as ModuleNameAttribute)?.Name ?? module.Name.Replace("Controller", string.Empty)
                });

        }
    }
}
