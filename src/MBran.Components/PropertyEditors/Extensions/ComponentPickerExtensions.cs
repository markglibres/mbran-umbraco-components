using MBran.Components.Constants;
using MBran.Components.Extensions;
using MBran.Components.PropertyEditors.Components;
using System.Web.Mvc;
using System.Web.Routing;

namespace MBran.Components.PropertyEditors.Extensions
{
    public static class ComponentPickerExtensions
    {
        public static MvcHtmlString Components(this HtmlHelper htmlHelper, ComponentPicker componentPicker)
        {
            var routeValues = new RouteValueDictionary {
                { RouteDataConstants.RenderOptions, componentPicker.RenderOption.Value }
            };
            return htmlHelper.Component(componentPicker?.ComponentType, componentPicker?.Sources, routeValues);
        }
    }
}
