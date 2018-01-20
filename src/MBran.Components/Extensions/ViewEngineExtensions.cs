using System.Web.Mvc;

namespace MBran.Components.Extensions
{
    public static class ViewEngineExtensions
    {
        public static bool ViewExists(this ViewEngineCollection viewEngines, 
            ControllerContext context, 
            string viewName,
            bool isPartial = false)
        {
            if(string.IsNullOrWhiteSpace(viewName) || context == null)
            {
                return false;
            }

            var engine = isPartial
                ? viewEngines.FindPartialView(context, viewName)
                : viewEngines.FindView(context, viewName, string.Empty);

            return engine.View != null;
        }
    }
}
