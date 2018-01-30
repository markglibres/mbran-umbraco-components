using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace MBran.Components.Api
{
    [PluginController("DocType")]
    public class DocTypeApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<DocTypeModel> GetAll()
        {
            var docTypes = Services.ContentTypeService.GetAllContentTypes();
            return docTypes.Select(docType => new DocTypeModel
            {
                Id = docType.Id,
                Name = docType.Name,
                Value = docType.Alias
            });
        }

        [HttpPost]
        public IEnumerable<DocTypeModel> GetDefinition([FromBody] string value)
        {
            var docTypes = Services.ContentTypeService.GetAllContentTypes();
            var docTypeAliases = value?.Split(',') ?? new string[0];
            return docTypes
                .Where(docType => docTypeAliases.Contains(docType.Alias, StringComparer.InvariantCultureIgnoreCase))
                .Select(docType => new DocTypeModel
                {
                    Id = docType.Id,
                    Name = docType.Name,
                    Value = docType.Alias
                });
        }
    }
}