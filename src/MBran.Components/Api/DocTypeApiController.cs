using MBran.Components.Helpers;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace MBran.Components.Models
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