using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Models;

namespace MBran.Components.Extensions
{
    public static class HtmlHelperComponentsExtensions
    {
        public static MvcHtmlString Component(this HtmlHelper helper, IEnumerable<IPublishedContent> components,
            RouteValueDictionary routeValues = null)
        {
            var stringBuilder = new StringBuilder();
            foreach (var component in components)
            {
                var htmlString = helper.Component(component,
                    routeValues != null ? new RouteValueDictionary(routeValues) : null
                ).ToHtmlString();

                stringBuilder.Append(htmlString);
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}