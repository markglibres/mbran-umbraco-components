using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using MBran.Components.Constants;
using MBran.Components.Controllers;
using Umbraco.Core.Models;

namespace MBran.Components.Extensions
{
    public static class HtmlHelperModuleExtensions
    {
        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            RouteValueDictionary routeValues = null)
            where T : IControllerRendering
        {
            return helper.Module<T>(string.Empty, null, routeValues);
        }

        public static MvcHtmlString Module(this HtmlHelper helper, Type module, IEnumerable<IPublishedContent> sources,
            RouteValueDictionary routeValues = null)
        {
            return helper.Module(module, string.Empty, sources, routeValues);
        }

        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            IEnumerable<IPublishedContent> model,
            RouteValueDictionary routeValues = null)
            where T : IControllerRendering
        {
            return helper.Module<T>(string.Empty, model, routeValues);
        }

        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            string viewPath,
            RouteValueDictionary routeValues = null)
            where T : IControllerRendering
        {
            return helper.Module<T>(viewPath, null, routeValues);
        }

        public static MvcHtmlString Module(this HtmlHelper helper, Type module,
            string viewPath,
            RouteValueDictionary routeValues = null)
        {
            return helper.Module(module, viewPath, null, routeValues);
        }

        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            string viewPath,
            IEnumerable<IPublishedContent> model,
            RouteValueDictionary routeValues = null)
            where T : IControllerRendering
        {
            return helper.Module(typeof(T), viewPath, model, routeValues);
        }

        public static MvcHtmlString Module(this HtmlHelper helper, Type module,
            string viewPath,
            IEnumerable<IPublishedContent> model,
            RouteValueDictionary routeValues = null)
        {
            var controller = module.Name.Replace("Controller", string.Empty);

            var options = routeValues ?? new RouteValueDictionary();
            options.Remove(RouteDataConstants.SourcesKey);
            options.Remove(RouteDataConstants.ViewPathKey);

            options.Add(RouteDataConstants.SourcesKey, model);
            options.Add(RouteDataConstants.ViewPathKey, viewPath);
            return helper.Action(nameof(IControllerRendering.Render), controller, options);
        }
    }
}