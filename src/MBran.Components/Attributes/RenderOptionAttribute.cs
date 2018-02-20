using System;
using Umbraco.Core;

namespace MBran.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RenderOptionAttribute : Attribute
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public RenderOptionAttribute(string name, string description="")
        {
            Name = name;
            Code = name.ToSafeAlias(false);
            Description = description;
        }

        
    }
}
