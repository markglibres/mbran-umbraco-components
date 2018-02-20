using System.Collections.Generic;
using System.Web.Http;
using MBran.Components.Helpers;
using MBran.Components.Models;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace MBran.Components.Api
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