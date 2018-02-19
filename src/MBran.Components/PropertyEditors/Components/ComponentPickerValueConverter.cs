using MBran.Components.Constants;
using MBran.Components.Helpers;
using MBran.Components.PropertyEditors.RenderOptions;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace MBran.Components.PropertyEditors.Components
{
    [PropertyValueType(typeof(ComponentPicker))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class ComponentPickerValueConverter : IPropertyValueConverter
    {
        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(PropertyEditorConstants.ComponentPicker.Alias,
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
            var targetDocTypeAlias = Convert.ToString(propertyValue[PropertyEditorConstants.ComponentPicker.Key]
                [PropertyEditorConstants.ComponentPicker.Value]);

            var sources = propertyValue[PropertyEditorConstants.ComponentPicker.Sources.Key]
                    .Select(sourceDoc => umbracoHelper.TypedContent(Convert.ToInt32(sourceDoc[PropertyEditorConstants.ComponentPicker.Sources.Id])));

            var compontentType = ModelsHelper.Instance.StronglyTypedPublishedContent(targetDocTypeAlias)
                                ?? ModelsHelper.Instance.StronglyTypedPocoByName(targetDocTypeAlias);

            var renderOptions = propertyValue[PropertyEditorConstants.RenderOptionPicker.Key];
            var renderOption = renderOptions.Any() ? new RenderOption
                {
                    Name = renderOptions[PropertyEditorConstants.RenderOptionPicker.Name]?.Value<string>() ?? string.Empty,
                    Value = renderOptions[PropertyEditorConstants.RenderOptionPicker.Value]?.Value<string>() ?? string.Empty,
                    Description = renderOptions[PropertyEditorConstants.RenderOptionPicker.Description]?.Value<string>() ?? string.Empty
                } : new RenderOption();

            var model = new ComponentPicker
            {
                Sources = sources,
                ComponentType = compontentType,
                RenderOption = renderOption
            };

            return model;
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return null;
        }
    }
}