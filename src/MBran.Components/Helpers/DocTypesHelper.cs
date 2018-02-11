﻿using MBran.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace MBran.Components.Helpers
{
    public class DocTypesHelper
    {
        private static Lazy<DocTypesHelper> _instance => new Lazy<DocTypesHelper>(() => new DocTypesHelper());
        public static DocTypesHelper Instance => _instance.Value;

        private DocTypesHelper() { }

        public IEnumerable<DocTypeDefinition> GetDocTypes(IContentTypeService contentTypeService)
        {
            string cacheName = string.Join("_", new[] {
                this.GetType().FullName,
                nameof(GetDocTypes)
            });

            return (IEnumerable<DocTypeDefinition>)ApplicationContext.Current
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheName, () => GetAllDocTypes(contentTypeService));
        }

        public IEnumerable<DocTypeDefinition> GetDocTypesDefinition(IContentTypeService contentTypeService, IEnumerable<string> docTypeAliases)
        {
            if(!docTypeAliases?.Any() ?? true) return new List<DocTypeDefinition>();

            return docTypeAliases.Select(docType => GetDocTypeDefinition(contentTypeService, docType));

        }

        private IEnumerable<DocTypeDefinition> GetAllDocTypes(IContentTypeService contentTypeService)
        {
            var docTypes = contentTypeService.GetAllContentTypes();
            return docTypes.Select(docType => new DocTypeDefinition
            {
                Id = docType.Id,
                Name = docType.Name,
                Value = docType.Alias
            });
        }

        private DocTypeDefinition GetDocTypeDefinition(IContentTypeService contentTypeService, string docTypeAlias)
        {
            string cacheName = string.Join("_", new[] {
                this.GetType().FullName,
                nameof(GetDocTypeDefinition),
                docTypeAlias
            });

            return (DocTypeDefinition)ApplicationContext.Current
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheName, () => {
                    var allDocTypes = contentTypeService.GetAllContentTypes();
                    return allDocTypes
                        .Where(docType => string.Equals(docType.Alias, docTypeAlias, StringComparison.InvariantCultureIgnoreCase))
                        .Select(docType => new DocTypeDefinition
                        {
                            Id = docType.Id,
                            Name = docType.Name,
                            Value = docType.Alias
                        })
                        .FirstOrDefault();
                });
        }
    }
}