using System.Collections.Generic;
using System.Web.Http;
using MBran.Components.Helpers;
using MBran.Components.Models;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace MBran.Components.Api
{
    [PluginController("MBranComponents")]
    public class DocTypeApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<DocTypeDefinition> GetAll()
        {
            return DocTypesHelper.Instance.GetDocTypes(Services.ContentTypeService);
        }

        [HttpPost]
        public IEnumerable<DocTypeDefinition> GetDefinition([FromBody] string value)
        {
            return DocTypesHelper.Instance.GetDocTypesDefinition(Services.ContentTypeService, value?.Split(','));
        }
    }
}