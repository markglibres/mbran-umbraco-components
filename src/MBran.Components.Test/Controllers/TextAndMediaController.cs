using MBran.Components.Attributes;
using MBran.Components.Controllers;
using System.Web.Mvc;

namespace MBran.Components.Test.Controllers
{
    public class TextAndMediaController : ComponentsController
    {
        [ViewOptions("Left", description:"Image is rendered at the left side.")]
        public PartialViewResult TextAndMediaLeft()
        {
            return Render();
        }

        [ViewOptions("Right", description: "Image is rendered at the right side.")]
        public PartialViewResult TextAndMediaRight()
        {
            return Render();
        }
    }
}