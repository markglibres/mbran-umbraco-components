﻿using System;
using System.Collections.Generic;
using System.Linq;
using MBran.Components.Constants;
using MBran.Components.Helpers;
using Newtonsoft.Json.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;

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
                return content
                    .Select(c => c.Map<T>())
                    .Where(c => c != null);
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
            return char.ToUpperInvariant(docType[0]) + docType.Substring(1);
        }

        public static string GetRenderOption(this IPublishedContent content)
        {
            //check if model has the renderOption property
            var property = content.GetProperty(PropertyEditorConstants.ComponentPicker.RenderOption.Key);
            //if has renderOption property, get value
            if (string.IsNullOrWhiteSpace(property?.DataValue?.ToString())) return string.Empty;

            var propertyValue =
                JObject.Parse(property.DataValue as string)[PropertyEditorConstants.RenderOptionPicker.Key];

            return propertyValue[PropertyEditorConstants.RenderOptionPicker.Value]?.Value<string>() ?? string.Empty;
        }
    }
}