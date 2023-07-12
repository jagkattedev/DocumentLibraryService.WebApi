using DocumentLibraryService.Common;
using DocumentLibraryService.Common.AppSettings;
using DocumentLibraryService.Common.Data;
using DocumentLibraryService.Common.Infrastructure.BusinessLogic;
using DocumentLibraryService.Common.Infrastructure.Interfaces.Dal.Repositories;
using DocumentLibraryService.WebApi.Common.Requests;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace DocumentLibraryService.BusinessLogic
{
    public class DocumentsLogic : BusinessLogicBase, IDocumentsLogic
    {
        public DocumentsLogic(AppConfiguration appConfig, IUnitOfWork unitOfWork): base(appConfig, unitOfWork)
        {
            
        }
 
        public async Task<bool> UploadDocuments(List<IFormFile> files)
        {
            bool result = false;
            if (files == null || files.Count == 0)
                return result;
            try
            {
                foreach (IFormFile file in files)
                {
                    var fileInfo = await UploadDocument(file);
                    await SaveDocumentDetails(file, fileInfo.Item1, fileInfo.Item2, fileInfo.Item3, AppConfig.GeneralSettings.ApiBaseUrl);
                }
                result = true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                result = false;
            }
            return result;
        }

        public async Task<List<UploadedDocument>> GetAllDocuments()
        {
            return await UnitOfWork.DocumentsRepository.GetAllUploadedDocuments();
        }

        public async Task<DocumentBytesInfo> GetDocumentBytes(string docId)
        {
            DocumentBytesInfo? docBytesInfo = null;
            var document = await UnitOfWork.DocumentsRepository.FindDocument(docId);
            if (document != null)
            {
                try
                {
                    var fileBytes = await System.IO.File.ReadAllBytesAsync(document.ServerAbsoluteFilePath);
                    docBytesInfo = new DocumentBytesInfo()
                    {
                        AbsoluteFilePath = document.ServerAbsoluteFilePath,
                        Bytes = fileBytes
                    };
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex.Message);
                    docBytesInfo = null;
                }
            }

            return docBytesInfo;


        }

        private async Task<Tuple<string,string, Guid>> UploadDocument(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            var fileGuid = Guid.NewGuid();
            string fileName = $"{fileGuid}{fileExtension}";

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles\\", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Tuple.Create(fileExtension, filePath, fileGuid);
        }

        private async Task SaveDocumentDetails(IFormFile file, string fileExtension, string filePath, Guid fileGuid, string apiBaseUrl)
        {
            var document = new UploadedDocument()
            {
                FileName = file.FileName,
                FileExtension = fileExtension,
                NumberOfDownloads = 0,
                ServerAbsoluteFilePath = filePath,
                UploadedOn = DateTime.UtcNow,
                FileUniqueIdentifierId = fileGuid
            };

            await UnitOfWork.DocumentsRepository.SaveUploadedDocumentDetails(document);

        }

        public async Task SaveShareLinkDetails(ShareLinkRequest request)
        {
            await UnitOfWork.DocumentsRepository.SaveShareLinkDetails(request);
        }

        public async Task<DocumentBytesInfo> ValidateAndFetchDocument(string shareId)
        {
            DocumentBytesInfo? documentBytesInfo = null;
            var documentLinkDetails = UnitOfWork.DocumentLinkMappingsRepository.GetDocumentLinkMappingDetails(shareId);
            if (documentLinkDetails.ValidUntil > DateTime.UtcNow)
            {
                var document = await UnitOfWork.DocumentsRepository.GetDocumentById(documentLinkDetails.DocumentId);
                documentBytesInfo = await GetDocumentBytes(document.FileUniqueIdentifierId?.ToString() ?? String.Empty);
            }
            return documentBytesInfo;
        }

        public async Task IncrementNumberOfDownloads(Guid fileUniqueIdentifierId)
        {
            await UnitOfWork.DocumentsRepository.IncrementNumberOfDownloads(fileUniqueIdentifierId);
        }
    }
}