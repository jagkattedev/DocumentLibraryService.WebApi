using DocumentLibraryService.Common.Data;
using DocumentLibraryService.WebApi.Common.Requests;

namespace DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories
{
    public interface IDocumentsRepository
    {
        public Task SaveUploadedDocumentDetails(UploadedDocument document);
        public Task<List<UploadedDocument>> GetAllUploadedDocuments();
        public Task<UploadedDocument> FindDocument(string documentId);
        Task SaveShareLinkDetails(ShareLinkRequest request);
        Task<UploadedDocument> GetDocumentById(long documentId);
         Task IncrementNumberOfDownloads(Guid? fileUniqueIdentifierId);
    }
}
