using System;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace MBran.Components.Picker
{
    public class ComponentPicker
    {
        public IEnumerable<IPublishedContent> Sources { get; set; }
        public Type ComponentType { get; set; }
    }
}
