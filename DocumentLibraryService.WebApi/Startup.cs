using DocumentLibraryService.BusinessLogic;
using DocumentLibraryService.Common.AppSettings;
using DocumentLibraryService.Common.Infrastructure.BusinessLogic;
using DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories;
using DocumentLibraryService.DataAccess;
using DocumentLibraryService.DataAccess.Entities;
using DocumentLibraryService.DataAccess.Repositories;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace DocumentLibraryService.WebApi
{
    public class Startup
    {

        private AppConfiguration _appConfig;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _appConfig = new AppConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }

        private string corsPolicy = "corsPolicy";    
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<AppConfiguration, AppConfiguration>();
            services.AddTransient<DocumentLibraryDbContext, DocumentLibraryDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDocumentsLogic, DocumentsLogic>();
            services.AddCors(options => options.AddPolicy(name: corsPolicy, builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseExceptionHandler("/error");
            app.UseRouting();
            app.UseCors(corsPolicy);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
          Path.Combine(env.ContentRootPath, "UploadedFiles")),
                RequestPath = "/UploadedFiles"
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitLogger();
        }

        private void InitLogger()
        {

            var logger = new Serilog.LoggerConfiguration().MinimumLevel.Information()
                                                          .WriteTo.File(_appConfig.SerilogSettings.SerilogLogFileLocation,
                                                              rollOnFileSizeLimit: true,
                                                              retainedFileCountLimit: _appConfig.SerilogSettings.SerilogRetainedFileCountLimit,
                                                              fileSizeLimitBytes: _appConfig.SerilogSettings.SerilogRollingFileSizeLimit)
                                                          .CreateLogger();
            Serilog.Log.Logger = logger;
        }
    }
}
   

