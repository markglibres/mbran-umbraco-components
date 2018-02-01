using System;

namespace MBran.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UmbracoModuleAttribute : Attribute
    {
        public UmbracoModuleAttribute(string moduleName, string description = "")
        {
            Name = moduleName;
            Description = description;
        }

        public virtual string Name { get; }
        public virtual string Description { get; }
    }
}