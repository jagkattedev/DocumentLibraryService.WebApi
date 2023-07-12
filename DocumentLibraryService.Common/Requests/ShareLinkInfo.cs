namespace DocumentLibraryService.WebApi.Common.Requests
{
    public class ShareLinkRequest
    {
        public string DocumentId { get; set; }
        public string ShareId { get; set; }

        public int ValidFor {get; set; }

        public ValidForUnits ValidForUnit { get; set; }

    }

    public enum ValidForUnits
    {
        Hours,
        Days
    }
}
