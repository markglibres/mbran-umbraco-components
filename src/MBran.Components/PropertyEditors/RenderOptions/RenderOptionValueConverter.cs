using MBran.Components.Constants;
using Newtonsoft.Json.Linq;
using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace MBran.Components.PropertyEditors.RenderOptions
{
    [PropertyValueType(typeof(RenderOption))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class RenderOptionValueConverter : IPropertyValueConverter
    {
        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(PropertyEditorConstants.RenderOptionPicker.Alias,
                StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            return Convert.ToString(source);
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            var propertyValue = JObject.Parse(source as string)[PropertyEditorConstants.RenderOptionPicker.Key];
            
            var model = new RenderOption
            {
                Name = propertyValue[PropertyEditorConstants.RenderOptionPicker.Name]?.Value<string>() ?? string.Empty,
                Value = propertyValue[PropertyEditorConstants.RenderOptionPicker.Value]?.Value<string>() ?? string.Empty,
                Description = propertyValue[PropertyEditorConstants.RenderOptionPicker.Description]?.Value<string>() ?? string.Empty
            };

            return model;
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            throw new NotImplementedException();
        }

        
    }
}
