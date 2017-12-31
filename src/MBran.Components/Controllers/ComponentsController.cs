using MBran.Components.Constants;
using MBran.Core.Extensions;
using MBran.Core.Helpers;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public class ComponentsController : SurfaceController, IComponentsController
    {
        public IPublishedContent PublishedContent => RouteData
                    .Values[RouteDataConstants.ModelKey] as IPublishedContent ?? CurrentPage;

        public string ComponentName => RouteData
                    .Values[RouteDataConstants.ComponentTypeKey] as string;

        public string ViewPath => RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string 
            ?? ComponentName;

        public virtual PartialViewResult Render()
        {
            return PartialView(ViewPath, PrepareModel());
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if (!this.PartialViewExists(viewName))
            {
                this.ControllerContext.RouteData.Values[RouteDataConstants.ControllerKey] = nameof(ComponentsController).Replace("Controller",string.Empty);
                this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = ComponentName;
            }

           return base.PartialView(viewName, model);
            
        }

        protected virtual object PrepareModel()
        {
            return PublishedContent.As(ModelsHelper.Instance.StronglyTyped(ComponentName));
        }

    }
}
