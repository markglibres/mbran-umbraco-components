using MBran.Components.Controllers;
using MBran.Core.Extensions;
using System;
using System.Linq;

namespace MBran.Components.Helpers
{
    public class ComponentsHelper
    {
        private static Lazy<ComponentsHelper> _helper => new Lazy<ComponentsHelper>(() => new ComponentsHelper());
        public static ComponentsHelper Instance => _helper.Value;

        private ComponentsHelper()
        {

        }

        public Type FindController(string docTypeAlias)
        {
            var docTypeController = docTypeAlias + "Controller";
            return AppDomain.CurrentDomain.FindImplementations<ComponentsController>()
                .Where(model => model.Name.Equals(docTypeController, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }
    }
}
