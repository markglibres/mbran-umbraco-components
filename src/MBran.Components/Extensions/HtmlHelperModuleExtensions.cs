using MBran.Components.Constants;
using MBran.Components.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Umbraco.Core.Models;

namespace MBran.Components.Extensions
{
    public static class HtmlHelperModuleExtensions
    {
        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            RouteValueDictionary routeValues = null)
            where T: IControllerRendering
        {
            return helper.Module<T>(string.Empty, null, routeValues);
        }

        public static MvcHtmlString Module(this HtmlHelper helper, Type module, IEnumerable<IPublishedContent> sources,
            RouteValueDictionary routeValues = null)
        {
            var options = routeValues ?? new RouteValueDictionary();
            options.Add(RouteDataConstants.SourcesKey, sources);
            return helper.Module(module, string.Empty, null, routeValues);
        }

        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            object model,
            RouteValueDictionary routeValues = null)
            where T : IControllerRendering
        {
            return helper.Module<T>(string.Empty, model, routeValues);
        }

        public static MvcHtmlString Module(this HtmlHelper helper, Type module,
            object model,
            RouteValueDictionary routeValues = null)
        {
            return helper.Module(module, string.Empty, model, routeValues);
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
            object model,
            RouteValueDictionary routeValues = null)
            where T : IControllerRendering
        {
            return helper.Module(typeof(T), viewPath, model, routeValues);
        }

        public static MvcHtmlString Module(this HtmlHelper helper, Type module,
            string viewPath,
            object model,
            RouteValueDictionary routeValues = null)
        {
            var options = routeValues ?? new RouteValueDictionary();
            options.Add(RouteDataConstants.ModelKey, model);
            options.Add(RouteDataConstants.ViewPathKey, viewPath);

            var controller = module.Name.Replace("Controller", string.Empty);
            return helper.Action(nameof(IControllerRendering.Render), controller, routeValues);
        }
    }
}
