using DocumentLibraryService.Common.Data;
using DocumentLibraryService.WebApi.Common.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibraryService.Common.Infrastructure.BusinessLogic
{
    public interface IDocumentsLogic
    {
        public Task<bool> UploadDocuments(List<IFormFile> files);
        Task<List<UploadedDocument>> GetAllDocuments();
        Task<DocumentBytesInfo> GetDocumentBytes(string docId);
        Task SaveShareLinkDetails(ShareLinkRequest request);
        Task<DocumentBytesInfo> ValidateAndFetchDocument(string shareId);

        Task IncrementNumberOfDownloads(Guid fileUniqueIdentifierId);
    }
}
