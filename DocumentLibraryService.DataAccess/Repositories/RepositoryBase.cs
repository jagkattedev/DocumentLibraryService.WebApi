
using DocumentLibraryService.Common.AppSettings;
using DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories;
using DocumentLibraryService.DataAccess.Entities;

namespace DocumentLibraryService.DataAccess.Repositories
{
    public class RepositoryBase 
    {
        public RepositoryBase(AppConfiguration appConfig, DocumentLibraryDbContext dBContext, IUnitOfWork unitOfWork)
        {
            AppConfig = appConfig;
            DBContext = dBContext;
            UnitOfWork = unitOfWork;
        }
        public AppConfiguration AppConfig { get; }
        public DocumentLibraryDbContext DBContext { get; }
        public IUnitOfWork UnitOfWork { get; }
    }
}
