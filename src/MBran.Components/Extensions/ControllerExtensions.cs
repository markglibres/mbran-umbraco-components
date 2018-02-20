using System.IO;
using System.Web.Mvc;

namespace MBran.Components.Extensions
{
    public static class ControllerExtensions
    {
        public static bool ViewExists(this Controller controller, string viewName)
        {
            return ViewEngines.Engines.ViewExists(controller.ControllerContext, viewName);
        }

        public static bool PartialViewExists(this Controller controller, string viewName)
        {
            return ViewEngines.Engines.ViewExists(controller.ControllerContext, viewName, true);
        }

        public static string GetName(this Controller controller)
        {
            return controller.GetType().Name.Replace("Controller", string.Empty);
        }

        public static string RenderViewToString(this Controller controller,
            string viewPath,
            object model = null,
            bool partial = false)
        {
            var viewEngine = controller.GetViewEngine(viewPath, partial);
            if (viewEngine == null)
                throw new FileNotFoundException("View cannot be found", viewPath);

            controller.SetViewEngineModel(model);
            var view = viewEngine.View;

            string result;
            using (var streamWriter = new StringWriter())
            {
                var viewContext = new ViewContext(controller.ControllerContext, view,
                    controller.ControllerContext.Controller.ViewData,
                    controller.ControllerContext.Controller.TempData,
                    streamWriter);

                view.Render(viewContext, streamWriter);
                result = streamWriter.ToString();
            }

            return result;
        }

        public static ViewEngineResult GetViewEngine(this Controller controller,
            string viewPath, bool partial = false)
        {
            return partial
                ? ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewPath)
                : ViewEngines.Engines.FindView(controller.ControllerContext, viewPath, null);
        }

        public static Controller SetViewEngineModel(this Controller controller, object model)
        {
            controller.ControllerContext.Controller.ViewData.Model = model;
            return controller;
        }
    }
}