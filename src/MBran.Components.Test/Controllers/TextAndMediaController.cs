using MBran.Components.Attributes;
using MBran.Components.Controllers;
using System.Web.Mvc;

namespace MBran.Components.Test.Controllers
{
    public class TextAndMediaController : ComponentsController
    {
        [RenderOption("Left", "Image is rendered at the left side.")]
        public PartialViewResult TextAndMediaLeft()
        {
            return Render();
        }

        [RenderOption("Right", "Image is rendered at the right side.")]
        public PartialViewResult TextAndMediaRight()
        {
            return Render();
        }
    }
}