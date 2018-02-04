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
        private string _moduleName => this.GetName();

        public virtual PartialViewResult Render()
        {
            return PartialView(GetViewPath(), CreateViewModel());
        }

        protected abstract object CreateViewModel();

        protected virtual string GetViewPath()
        {
            return RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string;
        }

        protected IPublishedContent GetModel()
        {
            return (RouteData.Values[RouteDataConstants.ModelKey] is IPublishedContent model) ?
            (model.Id > 0) ?
                model : CurrentPage
            : CurrentPage;
        }
        
        protected override PartialViewResult PartialView(string viewName, object model)
        {
            SetExecutingModuleFolder();

            if (string.IsNullOrWhiteSpace(viewName))
            {
                var moduleViewPath = $"~/Views/Modules/{_moduleName}/{_moduleName}.cshtml";
                if (this.PartialViewExists(moduleViewPath)) return base.PartialView(moduleViewPath, model);
                viewName = nameof(this.Render);
            }
            
            if (this.PartialViewExists(viewName)) return base.PartialView(viewName, model);

            this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = _moduleName;
            viewName = _moduleName;

            return base.PartialView(viewName, model);

        }

        public virtual IEnumerable<IPublishedContent> GetPublishedSources()
        {
            return (RouteData.Values[RouteDataConstants.SourcesKey] is IEnumerable<IPublishedContent> sources) ?
                sources : new List<IPublishedContent>();
        }

        private void SetExecutingModuleFolder()
        {
            ViewData[RouteDataConstants.ExecutingModule] = _moduleName;
        }
    }
}
