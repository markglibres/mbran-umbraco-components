using MBran.Components.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace MBran.Modules.PropertyEditors.Modules
{
    [PropertyValueType(typeof(ModulePickerValue))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class ModulePickerValueConverter : IPropertyValueConverter
    {
        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("MBran.Modules.Picker",
                StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            return Convert.ToString(source);
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            var propertyValue = JObject.Parse(source as string);
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var moduleType = Convert.ToString(propertyValue["module"]["value"]);

            var model = new ModulePickerValue
            {
                Sources = propertyValue["sources"]
                    .Select(sourceDoc => umbracoHelper.TypedContent(Convert.ToInt32(sourceDoc["id"]))),
                ModuleType = ModulesHelper.Instance.GetModuleType(moduleType)
            };

            return model;
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            throw new NotImplementedException();
        }
    }
}