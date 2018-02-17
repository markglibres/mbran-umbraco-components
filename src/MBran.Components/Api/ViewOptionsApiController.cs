using MBran.Components.Helpers;
using MBran.Components.Models;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace MBran.Components.Api
{
    [PluginController("MBranViewOptions")]
    public class ViewOptionsApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<ViewOptionsDefinition> GetComponentViews([FromUri]string docTypeAlias)
        {
            return DocTypesHelper.Instance.GetDocTypeViewOptions(docTypeAlias);
        }
    }
}
