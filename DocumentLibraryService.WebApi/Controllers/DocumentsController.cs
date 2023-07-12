using DocumentLibraryService.Common;
using DocumentLibraryService.Common.Infrastructure.BusinessLogic;
using DocumentLibraryService.Common.Response;
using DocumentLibraryService.WebApi.Common.Requests;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace DocumentLibraryService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private IDocumentsLogic _documentsLogic;
    
        public DocumentsController(IDocumentsLogic documentsLogic)
        {
            _documentsLogic = documentsLogic;
        }

        [HttpPost]
        public async Task<JsonResult> UploadDocuments([FromForm] IList<IFormFile> files)
        {
            Response response = new Response();
            if (Request.Form == null || Request.Form.Files.Count == 0)
            {
               response.IsSuccessful = false;
            }
            else
            {
                var docs = Request.Form.Files.ToList();
                await _documentsLogic.UploadDocuments(docs);
                response.IsSuccessful = true;
            }
            
            return new JsonResult(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllDocuments()
        {
            var docs = await _documentsLogic.GetAllDocuments();
            return new JsonResult(docs);
        }

        [HttpGet]
        public async Task<FileContentResult> DownloadDocument(string fileUniqueIdentifierId)
        {
            var docBytesInfo = await _documentsLogic.GetDocumentBytes(fileUniqueIdentifierId);
            var guid = new Guid(fileUniqueIdentifierId);
            await _documentsLogic.IncrementNumberOfDownloads(guid);
            if (docBytesInfo == null)
                return null;
            else
            {
                return File(docBytesInfo.Bytes, "text/plain", Path.GetFileName(docBytesInfo.AbsoluteFilePath));
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> ShareDocument(string shareId)
        {
            var docBytesInfo = await _documentsLogic.ValidateAndFetchDocument(shareId);
            if (docBytesInfo == null)
            {
                return new JsonResult("link has expired.");
            }
               
            else
            {
                return File(docBytesInfo.Bytes, "text/plain", Path.GetFileName(docBytesInfo.AbsoluteFilePath));
            }
        }

        [HttpPost]
        public async Task<JsonResult> ShareLink([FromBody] ShareLinkRequest request)
        {
            await _documentsLogic.SaveShareLinkDetails(request);
            var guid = new Guid(request.DocumentId);
            await _documentsLogic.IncrementNumberOfDownloads(guid);
            Response response = new Response();
            response.IsSuccessful = true;
            return new JsonResult(response);
        }

    }
}