using System;
using System.Linq;
using MBran.Components.Controllers;
using MBran.Components.Extensions;
using Umbraco.Core;

namespace MBran.Components.Helpers
{
    public class ComponentsHelper
    {
        private ComponentsHelper()
        {
        }

        private static Lazy<ComponentsHelper> _helper => new Lazy<ComponentsHelper>(() => new ComponentsHelper());
        public static ComponentsHelper Instance => _helper.Value;

        public Type FindController(string docTypeAlias)
        {
            return (Type)ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem(
                string.Join("_", new[] { this.GetType().FullName+ ".FindController", docTypeAlias }),
                () => GetController(docTypeAlias));
        }

        protected Type GetController(string docTypeAlias)
        {
             var docTypeController = docTypeAlias + "Controller";
            return AppDomain.CurrentDomain
                .FindImplementations<IControllerRendering>()
                .FirstOrDefault(model => model.Name.Equals(docTypeController, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}