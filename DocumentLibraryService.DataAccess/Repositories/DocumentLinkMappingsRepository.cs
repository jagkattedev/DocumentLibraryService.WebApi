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
    internal class DocumentLinkMappingsRepository : RepositoryBase, IDocumentLinkMappingsRepository
    {
        public DocumentLinkMappingsRepository(DocumentLibraryDbContext dbContext,
                               IUnitOfWork unitOfWork,
                               AppConfiguration appConfig) : base(appConfig, dbContext, unitOfWork)
        {

        }

        public DocumentLinkMappingDetails GetDocumentLinkMappingDetails(string shareId)
        {
            var linkMappingDetails = DBContext.DocumentLinkMappings.First(x => x.Id == new Guid(shareId));
            return linkMappingDetails.ConvertToDto();
        }
    }
}
