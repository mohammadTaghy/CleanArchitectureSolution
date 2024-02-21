using Application.Common.Exceptions;
using Application.Common.Model;
using Application.UseCases;
using Asp.Versioning;
using Common;
using Common.JWT;
using Infrastructure;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase, IBaseController
    {
        protected const string secretKey = "";
        protected readonly IMediator _mediator;
        protected readonly ICurrentUserSession _currentUserSession;

        public BaseController(
            IMediator mediator, 
            ICurrentUserSession currentUserSession)
        {
            _mediator = mediator;
            _currentUserSession = currentUserSession;
        }
        [HttpPost]
        [Authorize]
        //[RequestFormLimits(Order = 1000 * 1024 * 1024)]
        //[RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue,
        //    Order = int.MaxValue,MultipartHeadersLengthLimit =int.MaxValue, BufferBody =true,
        //    BufferBodyLengthLimit =int.MaxValue)]
        [DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        [ApiVersion(1.0)]
        public async Task<CommandResponse<FileUploadCommandDto>> UploadFile()
        {
            try
            {
                Request.EnableBuffering();
                IFormCollection formCollection = await Request.ReadFormAsync();
                IFormFileCollection files = formCollection.Files;
                //var boundary = HeaderUtilities.RemoveQuotes(Request.GetMultipartBoundary()).Value;
                //var reader = new MultipartReader(boundary, Request.Body);
                //var test= await reader.ReadNextSectionAsync();
                var file = files.First();// 
                string fileAdresses = string.Empty;
                //foreach (IFormFile file in files) {
                var folderName = Path.Combine("Asset", ControllerContext.ActionDescriptor.ControllerName);
                folderName = Path.Combine(folderName, Request.Form.Where(p => p.Key == "FileType").First().Value.First());

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                
                if (file.Length > 0)
                {
                    var fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{UtilizeFunction.GenerateNumberRandomCode(3)}" +
                        $"{Path.GetExtension(fileName)}";
                    var fullPath = Path.Combine(pathToSave, $"{fileName}");

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    fileAdresses=$@"{Request.Scheme}://{Request.Host.Value}/{Path.GetRelativePath(".", fullPath).Replace("\\", "/")}";
                }
                else
                {
                    throw new BadRequestException(CommonMessage.BadRequestException);
                }
                //}
                return new CommandResponse<FileUploadCommandDto>(true, new FileUploadCommandDto() { FileAddresses = fileAdresses });

            }
            catch (Exception ex)
            {
                throw new ValidationException();
            }
        }
        public class FileUploadCommandDto
        {
            public string FileAddresses { get; set; }
        }

    }
    public interface IBaseController
    {

    }
}
