using System.Web.Mvc;

namespace MBran.Components.Controllers
{
    public interface IControllerRendering
    {
        PartialViewResult Render();
        PartialViewResult RenderAs();
    }
}