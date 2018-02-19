using MBran.Components.Constants;
using MBran.Components.Controllers;
using MBran.Components.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MBran.Components.Extensions
{
    public static class HtmlHelperComponentExtensions
    {
        public static MvcHtmlString Component<T>(this HtmlHelper helper,
            RouteValueDictionary routeValues = null)
            where T: class
        {
            return helper.Component<T>(string.Empty, null, routeValues);
        }

        public static MvcHtmlString Component<T>(this HtmlHelper helper, object model,
            RouteValueDictionary routeValues = null)
            where T : class
        {
            return helper.Component<T>(string.Empty, model, routeValues);
        }

        public static MvcHtmlString Component<T>(this HtmlHelper helper, string viewPath,
            RouteValueDictionary routeValues = null)
            where T : class
        {
            return helper.Component<T>(viewPath, null, routeValues);
        }

        public static MvcHtmlString Component<T>(this HtmlHelper helper, string viewPath, object model,
            RouteValueDictionary routeValues = null)
            where T : class
        {
            return helper.Component(typeof(T), viewPath, new List<object> { model },  routeValues);
        }

        public static MvcHtmlString Component(this HtmlHelper helper,
            Type componentType, object model,
            RouteValueDictionary routeValues = null)
        {
            return helper.Component(componentType, string.Empty, new List<object> { model }, routeValues);
        }

        public static MvcHtmlString Component(this HtmlHelper helper,
            Type componentType, IEnumerable<object> models, 
            RouteValueDictionary routeValues = null)
        {
            return helper.Component(componentType, string.Empty, models, routeValues);
        }

        public static MvcHtmlString Component(this HtmlHelper helper,
            Type componentType, string viewPath, IEnumerable<object> models, 
            RouteValueDictionary routeValues = null)
        {
            if(componentType == null || models == null)
            {
                return new MvcHtmlString(string.Empty);
            }

            var htmlString = new StringBuilder();
            foreach(var model in models)
            {
                var htmlContent = helper.Component(componentType.Name, viewPath, model,
                    routeValues != null ? new RouteValueDictionary(routeValues) : routeValues,
                    componentType.AssemblyQualifiedName
                ).ToHtmlString();
                htmlString.Append(htmlContent);
            }

            return MvcHtmlString.Create(htmlString.ToString());
            
        }

        public static MvcHtmlString Component(this HtmlHelper helper, int nodeId,
            RouteValueDictionary routeValues = null)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var content = umbracoHelper.TypedContent(nodeId);
            return content == null ? new MvcHtmlString(string.Empty) : helper.Component(content, routeValues);
        }

        public static MvcHtmlString Component(this HtmlHelper helper, IPublishedContent model,
            RouteValueDictionary routeValues = null)
        {
            return helper.Component(model.GetDocumentTypeAlias(), string.Empty, model, routeValues, 
                model.GetType().AssemblyQualifiedName);
        }

        private static MvcHtmlString Component(this HtmlHelper helper, string componentName,
            string viewPath, object model,
            RouteValueDictionary routeValues = null,
            string componentFullname = null)
        {
            var controllerName = componentName;
            var componentController = ComponentsHelper.Instance.FindController(controllerName);
            if (componentController == null)
            {
                controllerName = nameof(ComponentsController).Replace("Controller", string.Empty);
            }

            var options = helper.CreateRouteValues(componentName, viewPath, model, routeValues, componentFullname);
            
            return helper.Action(nameof(IControllerRendering.RenderAs), controllerName, options);
        }

        private static RouteValueDictionary CreateRouteValues(this HtmlHelper helper, string componentName,
            string viewPath, object model,
            RouteValueDictionary routeValues = null,
            string componentFullname = null)
        {
            var options = routeValues ?? new RouteValueDictionary();
            options.Remove(RouteDataConstants.ComponentTypeKey);
            options.Remove(RouteDataConstants.ModelKey);
            options.Remove(RouteDataConstants.ViewPathKey);
            options.Remove(RouteDataConstants.ModelType);
            options.Remove(RouteDataConstants.ExecutingModule);

            options.Add(RouteDataConstants.ComponentTypeKey, componentName);
            options.Add(RouteDataConstants.ModelKey, model);
            options.Add(RouteDataConstants.ViewPathKey, viewPath);
            options.Add(RouteDataConstants.ModelType, componentFullname);
            options.Add(RouteDataConstants.ExecutingModule, helper.ViewData[RouteDataConstants.ExecutingModule]);
            
            return options;
        }

    }
}
