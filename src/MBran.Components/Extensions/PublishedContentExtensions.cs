using MBran.Components.Helpers;
using Our.Umbraco.Ditto;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using MBran.Components.Extensions;

namespace MBran.Components.Extensions
{
    public static class PublishedContentExtensions
    {
        public static T Map<T>(this IPublishedContent content)
            where T : class
        {
           return content.As<T>();
        }

        public static object Map(this IPublishedContent content, Type type)
        {
            return content.As(type);
        }

        public static IEnumerable<T> MapEnumerable<T>(this IEnumerable<IPublishedContent> content)
            where T : class
        {
            if (content != null)
            {
                return content
                    .Select(c => c.Map<T>())
                    .Where(c => c != null);
            }
            return new List<T>();
        }

        public static Type StronglyTyped(this IPublishedContent content)
        {
            var docTypeAlias = content.GetDocumentTypeAlias();
            return ModelsHelper.Instance.StronglyTypedPublishedContent(docTypeAlias);
        }

        public static string GetDocumentTypeAlias(this IPublishedContent content)
        {
            var docType = content.DocumentTypeAlias;
            return Char.ToUpperInvariant(docType[0]) + docType.Substring(1);

        }

        
    }
}
