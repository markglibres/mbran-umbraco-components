using System;
using Umbraco.Core;

namespace MBran.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ViewOptionsAttribute : Attribute
    {
        public string DisplayText { get; set; }
        public string ViewSuffix { get; set; }
        public string Description { get; set; }
        public ViewOptionsAttribute(string displayText, string viewSuffix="", string description="")
        {
            DisplayText = displayText;
            ViewSuffix = string.IsNullOrWhiteSpace(viewSuffix) ?
                displayText.ToSafeAlias(false) : viewSuffix;
            Description = description;
        }

        
    }
}
