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
            return ModulesHelper.Instance.GetDefinitions(value?.Split(','));
            
        }
    }
}