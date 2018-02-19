using MBran.Components.Attributes;
using MBran.Components.Constants;
using MBran.Components.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Umbraco.Core;

namespace MBran.Components.Extensions
{
    public static class ComponentExtensions
    {
        public static MethodInfo GetRenderAsMethod<T>(this T componentClass, string renderOptionValue)
            where T: ComponentsController, IControllerRendering
        {
            string cacheName = string.Join("_", new[] {
                typeof(ComponentExtensions).FullName,
                nameof(GetRenderAsMethod),
                componentClass.GetType().FullName,
                renderOptionValue
            });

            return (MethodInfo)ApplicationContext.Current
                .ApplicationCache
                .RuntimeCache
                .GetCacheItem(cacheName, () => {
                    var componentType = componentClass.GetType();
                    var renderMethod = componentType.GetMethods()
                        .FirstOrDefault(method =>
                            (method.GetCustomAttributes(typeof(RenderOptionAttribute), false) as IEnumerable<RenderOptionAttribute>)
                                .Any(attribute => attribute.Code.Equals(renderOptionValue, StringComparison.InvariantCultureIgnoreCase)));

                    return renderMethod;
                });
        }

        public static PartialViewResult RenderAsMethod<T>(this T componentClass, string renderOptionValue)
            where T : ComponentsController, IControllerRendering
        {
            
            var method = componentClass.GetRenderAsMethod(renderOptionValue);

            if (method == null) return null;

            componentClass.RouteData.Values[RouteDataConstants.ActionKey] = method.Name;
            return (PartialViewResult)method.Invoke(componentClass, null);
            
        }
    }
}
