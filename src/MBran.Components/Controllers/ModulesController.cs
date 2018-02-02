using MBran.Components.Constants;
using MBran.Components.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public abstract class ModulesController : SurfaceController, IModuleController, IControllerRendering
    {
        public virtual IPublishedContent Model => (RouteData.Values[RouteDataConstants.ModelKey] is IPublishedContent model) ?
            (model.Id > 0) ?
                model : CurrentPage
            : CurrentPage;

        public virtual string ViewPath => RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string;

        public virtual string ModuleName => this.GetName();

        public IEnumerable<IPublishedContent> PublishedContentSources => RouteData
                    .Values[RouteDataConstants.SourcesKey] as IEnumerable<IPublishedContent>;

        public virtual PartialViewResult Render()
        {
            return PartialView(ViewPath, CreateViewModel());
        }

        protected abstract object CreateViewModel();

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
