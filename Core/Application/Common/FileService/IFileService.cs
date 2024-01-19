using Application.UseCases;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Constants;

namespace Application.Common.FileService
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);


        public Task DownloadFileById(int fileName);
    }
}
