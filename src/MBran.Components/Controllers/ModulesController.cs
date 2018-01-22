using MBran.Components.Constants;
using MBran.Components.Extensions;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public abstract class ModulesController : SurfaceController, IControllerRendering
    {
        public virtual IPublishedContent Model => RouteData
                    .Values[RouteDataConstants.ModelKey] as IPublishedContent ?? CurrentPage;

        public virtual string ViewPath => RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string;

        public virtual string ModuleName => this.GetName();

        public virtual PartialViewResult Render()
        {
            return PartialView(ViewPath, GetViewModel());
        }

        protected abstract object GetViewModel();

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if(string.IsNullOrWhiteSpace(viewName))
            {
                viewName = nameof(this.Render);
            }

            if (this.PartialViewExists(viewName)) return base.PartialView(viewName, model);

            this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = ModuleName;
            viewName = ModuleName;

            return base.PartialView(viewName, model);

        }
        
    }
}
