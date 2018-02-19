using MBran.Components.Constants;
using MBran.Components.Extensions;
using MBran.Components.Helpers;
using System;
using System.Web.Mvc;
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

        private string GetViewName()
        {
            if (string.IsNullOrWhiteSpace(_viewName)) return GetComponentName();
            return _viewName;
                
        }

        private string GetActionName()
        {
            if (string.IsNullOrWhiteSpace(_actionName)) return GetComponentName();
            return _actionName;

        }

        public PartialViewResult RenderAs()
        {
            //get model
            var model = GetModel();
            var renderOption = string.IsNullOrWhiteSpace(_renderOption) ? model.GetRenderOption() : _renderOption;
            
            //get method based on renderOption value
            if (!string.IsNullOrWhiteSpace(renderOption))
            {
                var method = this.GetRenderAsMethod(renderOption);
                if (method != null)
                {
                    _viewName = method.Name;
                    _actionName = method.Name;
                    return (PartialViewResult)method.Invoke(this, null);
                }
            }

            return Render();
        }

        public virtual PartialViewResult Render()
        {
            return PartialView(GetViewPath(), CreateViewModel());
        }

        public virtual PartialViewResult RenderModel(object model)
        {
            return PartialView(GetViewPath(), model);
        }
        
        protected override PartialViewResult PartialView(string viewName, object model)
        {
            var partialView = GetPartialView(viewName, model);

            if(!string.IsNullOrWhiteSpace(partialView)) return base.PartialView(partialView, model);

            this.ControllerContext.RouteData.Values[RouteDataConstants.ControllerKey] = nameof(ComponentsController).Replace("Controller",string.Empty);
            this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = GetActionName();
            partialView = GetViewName();

            return base.PartialView(partialView, model);
            
        }

        private string GetPartialView(string viewName, object model)
        {
            string cacheName = string.Join("_", new[] {
                this.GetType().FullName,
                CurrentPage.GetDocumentTypeAlias(),GetComponentName(),GetExecutingModuleFolder(),
                GetViewName().ToSafeAlias()
            });

            return (string)ApplicationContext.Current
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheName, () => {
                    if (!string.IsNullOrWhiteSpace(viewName) && this.PartialViewExists(viewName)) return viewName;

                    var controllerViewPath = $"~/Views/{CurrentPage.GetDocumentTypeAlias()}/{GetViewName()}.cshtml";
                    if (this.PartialViewExists(controllerViewPath)) return controllerViewPath;

                    var moduleViewPath = $"~/Views/{GetExecutingModuleFolder()}/{GetViewName()}.cshtml";
                    if (this.PartialViewExists(moduleViewPath)) return moduleViewPath;

                    var componentFolderPath = $"~/Views/Components/{this.GetName()}/{GetViewName()}.cshtml";
                    if (this.PartialViewExists(componentFolderPath)) return componentFolderPath;

                    viewName = nameof(this.Render);
                    if (this.PartialViewExists(viewName)) return viewName;

                    return string.Empty;
                });
        }

        protected virtual object CreateViewModel()
        {
            var modelTypeQualifiedName = this.ControllerContext.RouteData.Values[RouteDataConstants.ModelType]?.ToString();
            Type modelType = null;
            if (!string.IsNullOrWhiteSpace(modelTypeQualifiedName))
            {
                modelType = Type.GetType(modelTypeQualifiedName);
            }

            if (modelType == null)
            {
                modelType = ModelsHelper.Instance.StronglyTypedPublishedContent(GetComponentName());
            }
            
            if (modelType == null)
            {
                throw new Exception($"Cannot find component {GetComponentName()}");
            }

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
