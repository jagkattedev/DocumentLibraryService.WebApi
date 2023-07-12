using DocumentLibraryService.Common;
using DocumentLibraryService.Common.Data;
using DocumentLibraryService.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibraryService.DataAccess
{
    public static class Extensions
    {
        public static Document ConvertToEntity(this UploadedDocument document)
        {
            var entity = new Document()
            {
                FileExtension = document.FileExtension,
                FileName = document.FileName,
                NumberOfDownloads = document.NumberOfDownloads,
                ServerAbsoluteFilePath = document.ServerAbsoluteFilePath,
                UploadedOn = document.UploadedOn,
                FileUniqueIdentifierId = document.FileUniqueIdentifierId,
            };

            return entity;
        }

        public static UploadedDocument ConvertToDto(this Document document, string apiBaseUrl)
        {
            string documentFetchUrl = $"{apiBaseUrl}/UploadedFiles/{document.FileUniqueIdentifierId}{document.FileExtension}";
            var dto = new UploadedDocument()
            {
                Id = document.Id,
                FileExtension = document.FileExtension,
                FileName = document.FileName,
                NumberOfDownloads = document.NumberOfDownloads,
                ServerAbsoluteFilePath = document.ServerAbsoluteFilePath,
                UploadedOn = document.UploadedOn,
                FileUniqueIdentifierId = document.FileUniqueIdentifierId,
                DocumentFetchUrl = documentFetchUrl,
                DocumentDownloadUrl = $"{apiBaseUrl}/api/Documents/DownloadDocument?fileUniqueIdentifierId={document.FileUniqueIdentifierId}"
            };

            return dto;
        }

        public static DocumentLinkMappingDetails ConvertToDto(this DocumentLinkMapping docMapping)
        {
            var dto = new DocumentLinkMappingDetails()
            {
                Id = docMapping.Id,
                DocumentId = docMapping.DocumentId,
                ValidUntil = docMapping.ValidUntil
            };

            return dto;
        }

       }
}
