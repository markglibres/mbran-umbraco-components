using System;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace MBran.Modules.PropertyEditors.Modules
{
    public class ModulePickerValue
    {
        public Type ModuleType { get; set; }
        public IEnumerable<IPublishedContent> Sources { get; set; }
    }
}
