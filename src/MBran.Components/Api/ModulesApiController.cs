using MBran.Components.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace MBran.Components.Models
{
    [PluginController("MBranModules")]
    public class ModulesApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<ModuleDefinition> GetAll()
        {
            return ModulesHelper.Instance.GetAll();
        }

        [HttpPost]
        public IEnumerable<ModuleDefinition> GetDefinition([FromBody] string value)
        {
            string[] moduleTypes = !string.IsNullOrWhiteSpace(value) ? value.Split(',') : new string[0];
            return ModulesHelper.Instance.GetAll()
                .Where(module => moduleTypes.Contains(module.Value,StringComparer.InvariantCultureIgnoreCase));
            
        }
    }
}