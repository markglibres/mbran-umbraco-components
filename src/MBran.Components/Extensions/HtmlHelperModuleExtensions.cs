using MBran.Components.Constants;
using MBran.Components.Controllers;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace MBran.Components.Extensions
{
    public static class HtmlHelperModuleExtensions
    {
        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            RouteValueDictionary routeValues = null)
            where T: IModulesController
        {
            return helper.Module<T>(string.Empty, null, routeValues);
        }

        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            object model,
            RouteValueDictionary routeValues = null)
            where T : IModulesController
        {
            return helper.Module<T>(string.Empty, model, routeValues);
        }

        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            string viewPath,
            RouteValueDictionary routeValues = null)
            where T : IModulesController
        {
            return helper.Module<T>(viewPath, null, routeValues);
        }

        public static MvcHtmlString Module<T>(this HtmlHelper helper,
            string viewPath,
            object model,
            RouteValueDictionary routeValues = null)
            where T : IModulesController
        {
            var options = routeValues ?? new RouteValueDictionary();
            options.Add(RouteDataConstants.ModelKey, model);
            options.Add(RouteDataConstants.ViewPathKey, viewPath);

            var controller = typeof(T).Name.Replace("Controller", string.Empty);
            return helper.Action(nameof(ModulesController.Render), controller, routeValues);
        }
        
    }
}
