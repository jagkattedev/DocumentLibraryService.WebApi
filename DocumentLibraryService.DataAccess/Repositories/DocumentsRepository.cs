using DocumentLibraryService.Common;
using DocumentLibraryService.Common.AppSettings;
using DocumentLibraryService.Common.Data;
using DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories;
using DocumentLibraryService.DataAccess;
using DocumentLibraryService.DataAccess.Entities;
using DocumentLibraryService.WebApi.Common.Requests;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibraryService.DataAccess.Repositories
{
    public class DocumentsRepository :RepositoryBase, IDocumentsRepository
    {
        public DocumentsRepository(DocumentLibraryDbContext dbContext,
                               IUnitOfWork unitOfWork,
                               AppConfiguration appConfig) : base(appConfig, dbContext, unitOfWork)
        {

        }
        public async Task SaveUploadedDocumentDetails(UploadedDocument document)
        {
            var documentInfo = document.ConvertToEntity();
            DBContext.Documents.Add(documentInfo);
            await DBContext.SaveChangesAsync();
        }

        public async Task<List<UploadedDocument>> GetAllUploadedDocuments()
        {
            return await DBContext.Documents.Select(x => x.ConvertToDto(AppConfig.GeneralSettings.ApiBaseUrl)).ToListAsync();
        }

        public async Task<UploadedDocument> FindDocument(string documentId)
        {
            var docId = new Guid(documentId);
            var document = await DBContext.Documents.FirstOrDefaultAsync(x => x.FileUniqueIdentifierId == docId);
            return document == null? null : document.ConvertToDto(AppConfig.GeneralSettings.ApiBaseUrl);
        }

        public async Task SaveShareLinkDetails(ShareLinkRequest request)
        {
            var document = await FindDocument(request.DocumentId);
            var validUntil = GetValidityDateTime(request.ValidFor, request.ValidForUnit);
            var linkMapping = new DocumentLinkMapping()
            {
                Id = new Guid(request.ShareId),
                DocumentId = document.Id,
                ValidUntil = validUntil
            };
            DBContext.DocumentLinkMappings.Add(linkMapping);
            await DBContext.SaveChangesAsync();
        }


        public async Task<UploadedDocument> GetDocumentById(long documentId)
        {
            var document = await DBContext.Documents.FirstOrDefaultAsync(x => x.Id == documentId);
            return document == null ? null : document.ConvertToDto(AppConfig.GeneralSettings.ApiBaseUrl);

        }

        private DateTime GetValidityDateTime(int validFor, ValidForUnits validForUnit)
        {
            DateTime validUntil;

            switch (validForUnit)
            {
                case ValidForUnits.Days:
                    {
                        validUntil = DateTime.UtcNow.AddDays(validFor);
                        break;
                    }
                case ValidForUnits.Hours:
                    {
                        validUntil = DateTime.UtcNow.AddHours(validFor);
                        break;
                    }
                default:
                    {
                        throw new Exception($"invalid enum value {validForUnit}");
                    }

            }

            return validUntil;
        }

        public async Task IncrementNumberOfDownloads(Guid? fileUniqueIdentifierId)
        {
           var document = DBContext.Documents.FirstOrDefault(x => x.FileUniqueIdentifierId == fileUniqueIdentifierId);
            if (document != null)
            {
                document.NumberOfDownloads += 1;
            }
            await DBContext.SaveChangesAsync();
        }
    }
}
