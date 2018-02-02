using System.Web.Mvc;
using MBran.Components.Attributes;
using MBran.Components.Controllers;

namespace MBran.Components.Test.Controllers
{
    [UmbracoModule("Test Module", "this is a test")]
    public class TestController : ModulesController
    {
        protected override object CreateViewModel()
        {
            return base.GetModel();
        }

        protected override string GetViewPath()
        {
            return base.GetViewPath();
        }

        public override PartialViewResult Render()
        {
            return base.Render();
        }
    }
}