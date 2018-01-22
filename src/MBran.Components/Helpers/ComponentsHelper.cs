using System;
using System.Linq;
using MBran.Components.Controllers;
using MBran.Components.Extensions;

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
            var docTypeController = docTypeAlias + "Controller";
            return AppDomain.CurrentDomain
                .FindImplementations<IControllerRendering>()
                .FirstOrDefault(model => model.Name.Equals(docTypeController, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}