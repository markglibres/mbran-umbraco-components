using MBran.Components.Controllers;

namespace MBran.Components.Test.Controllers
{
    public class TestController : ModulesController
    {
        protected override object GetViewModel()
        {
            return this.Model;
        }
    }
}