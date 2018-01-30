using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace MBran.Components.Api
{
    [PluginController("MBranModules")]
    public class ModulesApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<ModuleModel> GetAll()
        {
            var modules = Services.ContentTypeService.GetAllContentTypes();
            return new List<ModuleModel>
            {
                new ModuleModel {Name = "Module 1", Value = "this.is.my.type"},
                new ModuleModel {Name = "Module 2", Value = "this.is.my.type2"},
                new ModuleModel {Name = "Module 3", Value = "this.is.my.type3"}
            };
        }

        [HttpPost]
        public IEnumerable<ModuleModel> GetDefinition([FromBody] string value)
        {
            var modules = Services.ContentTypeService.GetAllContentTypes();
            var moduleNames = value?.Split(',') ?? new string[0];
            return new List<ModuleModel>
            {
                new ModuleModel {Name = "Module 1", Value = "this.is.my.type"},
                new ModuleModel {Name = "Module 2", Value = "this.is.my.type2"},
                new ModuleModel {Name = "Module 3", Value = "this.is.my.type3"}
            };
        }
    }
}