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
        private IPublishedContent _model => (RouteData.Values[RouteDataConstants.ModelKey] is IPublishedContent model) ?
            (model.Id > 0) ?
                model : CurrentPage
            : CurrentPage;

        private string _viewPath => RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string;

        private string _moduleName => this.GetName();

        public IEnumerable<IPublishedContent> PublishedContentSources => RouteData
                    .Values[RouteDataConstants.SourcesKey] as IEnumerable<IPublishedContent>;

        public virtual PartialViewResult Render()
        {
            return PartialView(GetViewPath(), CreateViewModel());
        }

        protected abstract object CreateViewModel();

        protected virtual string GetViewPath()
        {
            return _viewPath;
        }

        protected object GetModel()
        {
            return _model;
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if(string.IsNullOrWhiteSpace(viewName))
            {
                viewName = nameof(this.Render);
            }

            if (this.PartialViewExists(viewName)) return base.PartialView(viewName, model);

            this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = _moduleName;
            viewName = _moduleName;

            return base.PartialView(viewName, model);

        }
        
    }
}
