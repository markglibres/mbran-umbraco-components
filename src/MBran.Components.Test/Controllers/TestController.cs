using MBran.Components.Attributes;
using MBran.Components.Controllers;
using System.Collections.Generic;
using Umbraco.Core.Models;


namespace MBran.Components.Test.Controllers
{
    [UmbracoModule("Test Module", "this is a test")]
    public class TestController : ModulesController
    {
        protected override object CreateViewModel()
        {
            return base.GetPublishedSources();
        }

        protected override string GetViewPath()
        {
            return base.GetViewPath();
        }
        
        public override IEnumerable<IPublishedContent> GetPublishedSources()
        {
            return base.GetPublishedSources();
        }
    }
}