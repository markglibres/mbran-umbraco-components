using System;

namespace MBran.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UmbracoModuleAttribute : Attribute
    {
        public string Name { get; set;  }
        public string Description { get; set; }

        public UmbracoModuleAttribute(string moduleName, string description = "")
        {
            Name = moduleName;
            Description = description;
        }
        
    }
}