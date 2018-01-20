using System.Web.Mvc;
using Umbraco.Core.Models;

namespace MBran.Components.Controllers
{
    public interface IModulesController
    {
        IPublishedContent Model { get; }
        string ModuleName { get; }
        PartialViewResult Render();
        string ViewPath { get; }
    }
}
