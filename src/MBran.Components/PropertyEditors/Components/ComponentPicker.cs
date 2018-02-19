using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using MBran.Components.PropertyEditors.RenderOptions;

namespace MBran.Components.PropertyEditors.Components
{
    public class ComponentPicker
    {
        public IEnumerable<IPublishedContent> Sources { get; set; }
        public Type ComponentType { get; set; }
        public RenderOption RenderOption { get; set; }
    }
}
