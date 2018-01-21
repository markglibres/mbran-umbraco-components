using MBran.Components.Extensions;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace MBran.Components.Helpers
{
    public class ModelsHelper
    {
        private static Lazy<ModelsHelper> _helper => new Lazy<ModelsHelper>(() => new ModelsHelper());
        public static ModelsHelper Instance => _helper.Value;

        private ModelsHelper()
        {

        }

        public Type StronglyTypedPublishedContent(string docTypeAlias)
        {
            return AppDomain.CurrentDomain
                .FindImplementations<PublishedContentModel>()
                .FirstOrDefault(model => model.Name.Equals(docTypeAlias, StringComparison.InvariantCultureIgnoreCase));
        }

        public Type StronglyTypedPoco(string modelTypeFullName)
        {
            return !string.IsNullOrWhiteSpace(modelTypeFullName) ? AppDomain.CurrentDomain.FindImplementation(modelTypeFullName) 
                : null;
        }
    }
}
