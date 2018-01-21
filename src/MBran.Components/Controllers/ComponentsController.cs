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
        public virtual IPublishedContent Model => RouteData
                    .Values[RouteDataConstants.ModelKey] as IPublishedContent ?? CurrentPage;

        public virtual string ComponentName => RouteData
                    .Values[RouteDataConstants.ComponentTypeKey] as string;

        public virtual string ViewPath => RouteData
                    .Values[RouteDataConstants.ViewPathKey] as string 
            ?? ComponentName;

        public virtual PartialViewResult Render()
        {
            return PartialView(ViewPath, GetViewModel());
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                viewName = nameof(this.Render);
            }

            if (!this.PartialViewExists(viewName))
            {
                this.ControllerContext.RouteData.Values[RouteDataConstants.ControllerKey] = nameof(ComponentsController).Replace("Controller",string.Empty);
                this.ControllerContext.RouteData.Values[RouteDataConstants.ActionKey] = ComponentName;
                viewName = ComponentName;
            }

           return base.PartialView(viewName, model);
            
        }

        protected virtual object GetViewModel()
        {
            var modelType =
                ModelsHelper.Instance.StronglyTypedPoco(
                    this.ControllerContext.RouteData.Values[RouteDataConstants.ModelType]?.ToString())
                    ?? ModelsHelper.Instance.StronglyTypedPublishedContent(ComponentName);

            if (modelType == null)
            {
                throw new Exception($"Cannot find component {ComponentName}");
            }

            return Model.Map(modelType);
                
        }

    }
}
