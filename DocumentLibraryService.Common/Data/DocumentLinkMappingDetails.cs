namespace DocumentLibraryService.Common.Data
{
    public class DocumentLinkMappingDetails
    {
        public Guid Id { get; set; }

        public long DocumentId { get; set; }

        public DateTime ValidUntil { get; set; }
    }
}
