
namespace DocumentLibraryService.Common.Data
{
    public class UploadedDocument
    {
        public long Id { get; set; }
        public string FileName { get; set; } = null!;

        public string FileExtension { get; set; } = null!;

        public string ServerAbsoluteFilePath { get; set; } = null!;

        public DateTime UploadedOn { get; set; }

        public long NumberOfDownloads { get; set; }

        public Guid? FileUniqueIdentifierId { get; set; }

        public string DocumentDownloadUrl { get; set; } = null!;
        public string DocumentFetchUrl { get; set; } = null!;

    }
}
