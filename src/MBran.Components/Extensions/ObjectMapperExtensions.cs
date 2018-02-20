using System;
using System.Linq;
using System.Reflection;

namespace MBran.Components.Extensions
{
    public static class ObjectMapperExtensions
    {
        public static object MapAs(this object source, Type destinationType)
        {
            var destination = Activator.CreateInstance(destinationType);

            var destProps = destination.GetType().GetProperties()
                .Where(prop => prop.CanWrite);

            var validProps = source.GetType().GetProperties().Where(prop => prop.CanRead &&
                                                                            destProps.Any(propDest =>
                                                                                propDest.Name.Equals(prop.Name,
                                                                                    StringComparison
                                                                                        .InvariantCultureIgnoreCase)));

            foreach (var property in validProps)
                destination.GetType().GetProperty(property.Name,
                        BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
                    ?.SetValue(destination, property.GetValue(source));

            return destination;
        }
    }
}