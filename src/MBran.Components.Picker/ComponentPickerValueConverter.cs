using MBran.Components.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace MBran.Components.Picker
{
    [PropertyValueType(typeof(ComponentPickerValue))]
    [PropertyValueCache(PropertyCacheValue.All,PropertyCacheLevel.Content)]
    public class ComponentPickerValueConverter : IPropertyValueConverter
    {
        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("MBran.Components.Picker", StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            return Convert.ToString(source);
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            var propertyValue = JObject.Parse(source as string);
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var targetDocTypeAlias = Convert.ToString(propertyValue["component"]["value"]);

            var model = new ComponentPickerValue {
                Sources = propertyValue["sources"]
                    .Select(sourceDoc => umbracoHelper.TypedContent(Convert.ToInt32(sourceDoc["id"]))),
                TargetDocType = ModelsHelper.Instance.StronglyTypedPublishedContent(targetDocTypeAlias)
                    ?? ModelsHelper.Instance.StronglyTypedPocoByName(targetDocTypeAlias)

            };

            return model;

        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return null;
        }

        
    }
}
