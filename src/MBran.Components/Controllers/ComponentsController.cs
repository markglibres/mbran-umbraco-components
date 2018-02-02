using MBran.Components.Constants;
using MBran.Components.Extensions;
using MBran.Components.Helpers;
using System;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace MBran.Components.Controllers
{
    public class ComponentsController : SurfaceController, IControllerRendering
    {
        private IPublishedContent _model => RouteData
                    .Values[RouteDataConstants.ModelKey] as IPublishedContent ?? CurrentPage;

        private string _componentName => RouteData
                    .Values[RouteDataConstants.ComponentTypeKey] as string;

        private string _viewPath => RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string 
            ?? _componentName;

        public virtual PartialViewResult Render()
        {
            return PartialView(GetViewPath(), CreateViewModel());
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                viewName = nameof(this.Render);
            }

            if (this.PartialViewExists(viewName)) return base.PartialView(viewName, model);

            this.ControllerContext.RouteData.Values[RouteDataConstants.ControllerKey] = nameof(ComponentsController).Replace("Controller",string.Empty);
            this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = _componentName;
            viewName = _componentName;

            return base.PartialView(viewName, model);
            
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

            return _model.Map(modelType);
                
        }

        protected virtual string GetViewPath()
        {
            return _viewPath;
        }

        protected object GetModel()
        {
            return _model;
        }

    }
}
