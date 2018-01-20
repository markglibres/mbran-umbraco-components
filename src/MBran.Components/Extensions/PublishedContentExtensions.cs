using MBran.Components.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace MBran.Components.Extensions
{
    public static class PublishedContentExtensions
    {
        public static T As<T>(this IPublishedContent content)
            where T : PublishedContentModel
        {
            var newObject = (T)Activator.CreateInstance(typeof(T), content);
            return newObject;
        }

        public static object As(this IPublishedContent content, Type type)
        {
            var newObject = Activator.CreateInstance(type, content);
            return newObject;
        }

        public static IEnumerable<T> ConvertTo<T>(this IEnumerable<IPublishedContent> content)
            where T : PublishedContentModel
        {
            if (content != null)
            {
                return content
                    .Select(c => c.As<T>())
                    .Where(c => c != null);
            }
            return new List<T>();
        }

        public static Type StronglyTyped(this IPublishedContent content)
        {
            var docTypeAlias = content.GetDocumentTypeAlias();
            return ModelsHelper.Instance.StronglyTyped(docTypeAlias);
        }

        public static string GetDocumentTypeAlias(this IPublishedContent content)
        {
            var docType = content.DocumentTypeAlias;
            return Char.ToUpperInvariant(docType[0]) + docType.Substring(1);

        }

        
    }
}
