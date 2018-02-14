using MBran.Components.Extensions;
using System.Web.Mvc;

namespace MBran.Components.Picker
{
    public static class ComponentPickerExtensions
    {
        public static MvcHtmlString RenderComponents(this HtmlHelper htmlHelper, ComponentPicker componentPicker)
        {

            return htmlHelper.Component(componentPicker?.ComponentType, componentPicker?.Sources);
        }
    }
}
