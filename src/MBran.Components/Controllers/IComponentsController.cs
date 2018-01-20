using System.Web.Mvc;
using Umbraco.Core.Models;

namespace MBran.Components.Controllers
{
    public interface IComponentsController
    {
        IPublishedContent Model { get; }
        string ComponentName { get; }
        PartialViewResult Render();
        string ViewPath { get; }
    }
}
