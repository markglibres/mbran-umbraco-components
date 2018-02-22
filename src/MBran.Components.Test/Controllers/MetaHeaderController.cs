using MBran.Components.Controllers;
using MBran.Components.Test.Pocos;
using Our.Umbraco.Ditto;

namespace MBran.Components.Test.Controllers
{
    public class MetaHeader2Controller : ComponentsController
    {
        protected override object CreateViewModel()
        {
            var viewModel = GetModel().As<MetaHeader>();
            viewModel.MetaTitle = viewModel.MetaTitle + $" {GetType().Name}";
            return viewModel;
        }
    }
}