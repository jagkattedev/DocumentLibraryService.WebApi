using DocumentLibraryService.Common.AppSettings;
using DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories;
using DocumentLibraryService.DataAccess.Entities;
using DocumentLibraryService.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibraryService.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DocumentLibraryDbContext _dbContext;
        private AppConfiguration _appConfig;
        public UnitOfWork(AppConfiguration appConfig)
        {
            _appConfig = appConfig;
            _dbContext = new DocumentLibraryDbContext(_appConfig);
            DocumentsRepository = new DocumentsRepository(_dbContext, this, _appConfig);
            DocumentLinkMappingsRepository = new DocumentLinkMappingsRepository(_dbContext, this, _appConfig);
        }

        public IDocumentsRepository? DocumentsRepository { get; private set; }
        public IDocumentLinkMappingsRepository DocumentLinkMappingsRepository { get; private set;}

        public void Dispose()
        {
            DocumentsRepository = null;
        }
    }
}
