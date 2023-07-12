using DocumentLibraryService.Common.AppSettings;
using DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibraryService.BusinessLogic
{
    public class BusinessLogicBase
    {
        public BusinessLogicBase(AppConfiguration appConfig, IUnitOfWork unitOfWork)
        {
            AppConfig = appConfig;
            UnitOfWork = unitOfWork;
        }
        public AppConfiguration AppConfig { get; }
        public IUnitOfWork UnitOfWork { get; }
    }
}
