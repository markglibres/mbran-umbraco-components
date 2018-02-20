using System;
using System.Web.Mvc;
using MBran.Components.Constants;
using MBran.Components.Extensions;
using MBran.Components.Helpers;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public class ComponentsController : SurfaceController, IControllerRendering
    {
        private string _viewName { get; set; }
        private string _actionName { get; set; }

        private string _renderOption => RouteData
            .Values[RouteDataConstants.RenderOptions] as string;

        public PartialViewResult RenderAs()
        {
            //get model
            var model = GetModel();
            var renderOption = string.IsNullOrWhiteSpace(_renderOption) ? model.GetRenderOption() : _renderOption;

            //get method based on renderOption value
            if (string.IsNullOrWhiteSpace(renderOption)) return Render();

            var method = this.GetRenderAsMethod(renderOption);
            if (method == null) return Render();

            _viewName = method.Name;
            _actionName = method.Name;
            return (PartialViewResult) method.Invoke(this, null);
        }

        public virtual PartialViewResult Render()
        {
            return PartialView(GetViewPath(), CreateViewModel());
        }

        private string GetViewName()
        {
            return string.IsNullOrWhiteSpace(_viewName) ? GetComponentName() : _viewName;
        }

        private string GetActionName()
        {
            return string.IsNullOrWhiteSpace(_actionName) ? GetComponentName() : _actionName;
        }

        public virtual PartialViewResult RenderModel(object model)
        {
            return PartialView(GetViewPath(), model);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            var partialView = GetPartialView(viewName);

            if (!string.IsNullOrWhiteSpace(partialView)) return base.PartialView(partialView, model);

            ControllerContext.RouteData.Values[RouteDataConstants.ControllerKey] =
                nameof(ComponentsController).Replace("Controller", string.Empty);
            ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = GetActionName();
            partialView = GetViewName();

            return base.PartialView(partialView, model);
        }

        private string GetPartialView(string viewName)
        {
            var cacheName = string.Join("_", GetType().FullName, CurrentPage.GetDocumentTypeAlias(),
                GetComponentName(), GetExecutingModuleFolder(), GetViewName().ToSafeAlias());

            return (string) ApplicationContext.Current
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheName, () =>
                {
                    if (!string.IsNullOrWhiteSpace(viewName) && this.PartialViewExists(viewName)) return viewName;

                    var controllerViewPath = $"~/Views/{CurrentPage.GetDocumentTypeAlias()}/{GetViewName()}.cshtml";
                    if (this.PartialViewExists(controllerViewPath)) return controllerViewPath;

                    var moduleViewPath = $"~/Views/{GetExecutingModuleFolder()}/{GetViewName()}.cshtml";
                    if (this.PartialViewExists(moduleViewPath)) return moduleViewPath;

                    var componentFolderPath = $"~/Views/Components/{this.GetName()}/{GetViewName()}.cshtml";
                    if (this.PartialViewExists(componentFolderPath)) return componentFolderPath;

                    viewName = nameof(Render);
                    return this.PartialViewExists(viewName) ? viewName : string.Empty;
                });
        }

        protected virtual object CreateViewModel()
        {
            var modelTypeQualifiedName = ControllerContext.RouteData.Values[RouteDataConstants.ModelType]?.ToString();
            Type modelType = null;
            if (!string.IsNullOrWhiteSpace(modelTypeQualifiedName))
                modelType = Type.GetType(modelTypeQualifiedName);

            if (modelType == null)
                modelType = ModelsHelper.Instance.StronglyTypedPublishedContent(GetComponentName());

            if (modelType == null)
                throw new Exception($"Cannot find component {GetComponentName()}");

            return GetModel().Map(modelType);
        }

        protected virtual string GetViewPath()
        {
            return RouteData
                       .Values[RouteDataConstants.ViewPathKey] as string
                   ?? GetViewName();
        }

        protected IPublishedContent GetModel()
        {
            return RouteData
                       .Values[RouteDataConstants.ModelKey] as IPublishedContent ?? CurrentPage;
        }

        protected string GetExecutingModuleFolder()
        {
            return RouteData
                .Values[RouteDataConstants.ExecutingModule] as string;
        }

        protected string GetComponentName()
        {
            return RouteData
                .Values[RouteDataConstants.ComponentTypeKey] as string;
        }
    }
}