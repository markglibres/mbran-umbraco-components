using System;
using System.Linq;
using MBran.Components.Extensions;
using Umbraco.Core.Models.PublishedContent;

namespace MBran.Components.Helpers
{
    public class ModelsHelper
    {
        private ModelsHelper()
        {
        }

        private static Lazy<ModelsHelper> _helper => new Lazy<ModelsHelper>(() => new ModelsHelper());
        public static ModelsHelper Instance => _helper.Value;

        public Type StronglyTypedPublishedContent(string docTypeAlias)
        {
            return AppDomain.CurrentDomain
                .FindImplementations<PublishedContentModel>()
                .FirstOrDefault(model => model.Name.Equals(docTypeAlias, StringComparison.InvariantCultureIgnoreCase));
        }

        public Type StronglyTypedPoco(string modelTypeFullName)
        {
            return !string.IsNullOrWhiteSpace(modelTypeFullName)
                ? AppDomain.CurrentDomain.FindImplementation(modelTypeFullName)
                : null;
        }

        public Type StronglyTypedPocoByName(string typeName)
        {
            return !string.IsNullOrWhiteSpace(typeName)
                ? AppDomain.CurrentDomain.FindImplementations(typeName).FirstOrDefault()
                : null;
        }
    }
}