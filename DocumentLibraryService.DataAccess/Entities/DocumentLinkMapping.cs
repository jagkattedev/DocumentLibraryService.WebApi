using System;
using System.Collections.Generic;

namespace DocumentLibraryService.DataAccess.Entities;

public partial class DocumentLinkMapping
{
    public Guid Id { get; set; }

    public long DocumentId { get; set; }

    public DateTime ValidUntil { get; set; }

    public virtual Document Document { get; set; } = null!;
}
