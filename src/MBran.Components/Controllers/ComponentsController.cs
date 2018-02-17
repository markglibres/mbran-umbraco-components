using MBran.Components.Constants;
using MBran.Components.Extensions;
using MBran.Components.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public class ComponentsController : SurfaceController, IControllerRendering
    {
        private string _componentName => RouteData
                    .Values[RouteDataConstants.ComponentTypeKey] as string;

        public virtual PartialViewResult Render()
        {
            return PartialView(GetViewPath(), CreateViewModel());
        }

        public virtual PartialViewResult Render(object model)
        {
            return PartialView(GetViewPath(), model);
        }
        
        protected override PartialViewResult PartialView(string viewName, object model)
        {
            var partialView = GetPartialView(viewName, model);

            if(!string.IsNullOrWhiteSpace(partialView)) return base.PartialView(partialView, model);

            this.ControllerContext.RouteData.Values[RouteDataConstants.ControllerKey] = nameof(ComponentsController).Replace("Controller",string.Empty);
            this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = _componentName;
            partialView = _componentName;

            return base.PartialView(partialView, model);
            
        }

        private string GetPartialView(string viewName, object model)
        {
            if (!string.IsNullOrWhiteSpace(viewName) && this.PartialViewExists(viewName)) return viewName;

            var controllerViewPath = $"~/Views/{CurrentPage.GetDocumentTypeAlias()}/{_componentName}.cshtml";
            if (this.PartialViewExists(controllerViewPath)) return controllerViewPath;

            var moduleViewPath = $"~/Views/{GetExecutingModuleFolder()}/{_componentName}.cshtml";
            if (this.PartialViewExists(moduleViewPath)) return moduleViewPath;

            viewName = nameof(this.Render);
            if (this.PartialViewExists(viewName)) return viewName;

            return string.Empty;
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
                modelType = ModelsHelper.Instance.StronglyTypedPublishedContent(_componentName);
            }
            
            if (modelType == null)
            {
                throw new Exception($"Cannot find component {_componentName}");
            }

            return GetModel().Map(modelType);
                
        }

        protected virtual string GetViewPath()
        {
            return RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string
            ?? _componentName;
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

    }
}
