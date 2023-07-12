using System;
using System.Collections.Generic;

namespace DocumentLibraryService.DataAccess.Entities;

public partial class Document
{
    public long Id { get; set; }

    public string FileName { get; set; } = null!;

    public string FileExtension { get; set; } = null!;

    public string ServerAbsoluteFilePath { get; set; } = null!;

    public DateTime UploadedOn { get; set; }

    public long NumberOfDownloads { get; set; }

    public Guid? FileUniqueIdentifierId { get; set; }

    public virtual ICollection<DocumentLinkMapping> DocumentLinkMappings { get; set; } = new List<DocumentLinkMapping>();
}
