using MBran.Components.Constants;
using MBran.Components.Controllers;
using MBran.Components.Helpers;
using MBran.Core.Extensions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace MBran.Components.Extensions
{
    public static class HtmlHelperComponentExtensions
    {
        public static MvcHtmlString Component<T>(this HtmlHelper helper,
            RouteValueDictionary routeValues = null)
            where T: PublishedContentModel, IPublishedContent
        {
            return helper.Component<T>(string.Empty, null, routeValues);
        }

        public static MvcHtmlString Component<T>(this HtmlHelper helper, object model,
            RouteValueDictionary routeValues = null)
            where T : PublishedContentModel, IPublishedContent
        {
            return helper.Component<T>(string.Empty, model, routeValues);
        }

        public static MvcHtmlString Component<T>(this HtmlHelper helper, string viewPath,
            RouteValueDictionary routeValues = null)
            where T : PublishedContentModel, IPublishedContent
        {
            return helper.Component<T>(viewPath, null, routeValues);
        }

        public static MvcHtmlString Component<T>(this HtmlHelper helper, string viewPath, object model,
            RouteValueDictionary routeValues = null)
            where T : PublishedContentModel, IPublishedContent
        {
            return helper.Component(typeof(T).Name, viewPath, model, routeValues);
        }

        public static MvcHtmlString Component(this HtmlHelper helper, IPublishedContent model,
            RouteValueDictionary routeValues = null)
        {
            return helper.Component(model.GetDocumentTypeAlias(), string.Empty, model, routeValues);
        }

        public static MvcHtmlString Component(this HtmlHelper helper, int nodeId,
            RouteValueDictionary routeValues = null)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var content = umbracoHelper.TypedContent(nodeId);
            if(content == null)
            {
                return new MvcHtmlString(string.Empty);
            }
            return helper.Component(content, routeValues);

        }

        private static MvcHtmlString Component(this HtmlHelper helper, string componentName,
            string viewPath, object model,
            RouteValueDictionary routeValues = null)
        {

            var controllerName = componentName;
            var componentController = ComponentsHelper.Instance.FindController(controllerName);
            if (componentController == null)
            {
                controllerName = nameof(ComponentsController).Replace("Controller", string.Empty);
            }

            var options = routeValues ?? new RouteValueDictionary();
            options.Add(RouteDataConstants.ComponentTypeKey, componentName);
            options.Add(RouteDataConstants.ModelKey, model);
            options.Add(RouteDataConstants.ViewPathKey, viewPath);

            return helper.Action(nameof(ComponentsController.Render), controllerName, options);
        }

    }
}
