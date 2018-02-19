using System;
using Umbraco.Core;

namespace MBran.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RenderOptionAttribute : Attribute
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public RenderOptionAttribute(string name, string description="")
        {
            Name = name;
            Code = name.ToSafeAlias(false);
            Description = description;
        }

        
    }
}
