using System.Collections.Generic;
using Umbraco.Core.Models;

namespace MBran.Components.Controllers
{
    public interface IModuleController
    {
        IEnumerable<IPublishedContent> GetPublishedSources();
    }
}
