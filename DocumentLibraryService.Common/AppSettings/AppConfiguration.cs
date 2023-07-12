
using Microsoft.Extensions.Configuration;

namespace DocumentLibraryService.Common.AppSettings
{
    public class AppConfiguration
    {
        private IConfiguration _configuration;
        public AppConfiguration(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }
        public GeneralSettings GeneralSettings => _configuration.GetSection(nameof(GeneralSettings)).Get<GeneralSettings>();
        public SerilogSettings SerilogSettings => _configuration.GetSection(nameof(SerilogSettings)).Get<SerilogSettings>();
        public DbSettings DbSettings => _configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();

    }
}
