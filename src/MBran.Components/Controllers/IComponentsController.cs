using System.Web.Mvc;
using Umbraco.Core.Models;

namespace MBran.Components.Controllers
{
    public interface IComponentsController
    {
        IPublishedContent PublishedContent { get; }
        string ComponentName { get; }
        PartialViewResult Render();
        string ViewPath { get; }
    }
}
