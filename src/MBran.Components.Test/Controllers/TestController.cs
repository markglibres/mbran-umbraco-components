using MBran.Components.Attributes;
using MBran.Components.Controllers;

namespace MBran.Components.Test.Controllers
{
    [ModuleName("Test Module")]
    public class TestController : ModulesController
    {
        protected override object GetViewModel()
        {
            return this.Model;
        }
    }
}