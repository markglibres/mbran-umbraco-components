using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MBran.Components.Constants;
using MBran.Components.Extensions;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public abstract class ModulesController : SurfaceController, IModuleController, IControllerRendering
    {
        private string _moduleName => this.GetName();

        public PartialViewResult Render()
        {
            return PartialView(GetViewPath(), CreateViewModel());
        }

        public PartialViewResult RenderAs()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IPublishedContent> GetPublishedSources()
        {
            return RouteData.Values[RouteDataConstants.SourcesKey] is IEnumerable<IPublishedContent> sources
                ? sources
                : new List<IPublishedContent> {CurrentPage};
        }

        protected abstract object CreateViewModel();

        protected virtual string GetViewPath()
        {
            return RouteData
                .Values[RouteDataConstants.ViewPathKey] as string;
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            SetExecutingModuleFolder();

            var partialView = GetPartialView(viewName);

            if (!string.IsNullOrWhiteSpace(partialView)) return base.PartialView(partialView, model);

            ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = _moduleName;
            partialView = _moduleName;

            return base.PartialView(partialView, model);
        }

        private string GetPartialView(string viewName)
        {
            var cacheName = string.Join("_", GetType().FullName, CurrentPage.GetDocumentTypeAlias(),
                _moduleName, viewName.ToSafeAlias());

            return (string) ApplicationContext.Current
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheName, () =>
                {
                    if (!string.IsNullOrWhiteSpace(viewName) && this.PartialViewExists(viewName)) return viewName;

                    var moduleViewPath = $"~/Views/Modules/{_moduleName}/{_moduleName}.cshtml";

                    if (this.PartialViewExists(moduleViewPath)) return moduleViewPath;

                    viewName = nameof(Render);
                    return this.PartialViewExists(viewName) ? viewName : string.Empty;
                });
        }

        private void SetExecutingModuleFolder()
        {
            ViewData[RouteDataConstants.ExecutingModule] = _moduleName;
        }
    }
}