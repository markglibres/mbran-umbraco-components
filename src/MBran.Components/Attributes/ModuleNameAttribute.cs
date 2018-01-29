using System;

namespace MBran.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleNameAttribute : Attribute
    {
        public ModuleNameAttribute(string moduleName)
        {
            Name = moduleName;
        }

        public virtual string Name { get; }
    }
}