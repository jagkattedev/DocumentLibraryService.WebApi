namespace DocumentLibraryService.Common.AppSettings
{
    public class SerilogSettings
    {
        public int SerilogRetainedFileCountLimit { get; set; }
        public long SerilogRollingFileSizeLimit { get; set; }
        public string SerilogLogFileLocation { get; set; }
    }
}
