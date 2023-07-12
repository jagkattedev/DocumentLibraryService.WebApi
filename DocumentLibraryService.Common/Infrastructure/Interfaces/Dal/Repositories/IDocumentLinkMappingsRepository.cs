using DocumentLibraryService.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories
{
    public interface IDocumentLinkMappingsRepository
    {
        public DocumentLinkMappingDetails GetDocumentLinkMappingDetails(string shareId);
    }
}
