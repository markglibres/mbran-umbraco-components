using MBran.Components.Constants;
using MBran.Core.Extensions;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public abstract class ModulesController : SurfaceController, IModulesController
    {
        public IPublishedContent PublishedContent => RouteData
                    .Values[RouteDataConstants.ModelKey] as IPublishedContent ?? CurrentPage;

        public string ViewPath => RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string;

        public string ModuleName => this.GetName();

        public virtual PartialViewResult Render()
        {
            return PartialView(ViewPath, PrepareModel());
        }

        protected abstract object PrepareModel();

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if (!this.PartialViewExists(viewName))
            {
                this.ControllerContext.RouteData.Values[RouteDataConstants.ControllerKey] = ModuleName;
                this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = ModuleName;
            }

            return base.PartialView(viewName, model);

        }
    }
}
