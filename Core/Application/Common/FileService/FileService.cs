//using Application.UseCases;
//using Common;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Common.FileService
//{
//    internal class FileService : IFileService
//    {
       

//        public async Task PostFileAsync(IFormFile fileData, Constants.FileType fileType)
//        {
//            try
//            {
               

//                using (var stream = new MemoryStream())
//                {
//                    fileData.CopyTo(stream);
//                    stream.Write()
//                }

//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task PostMultiFileAsync(List<FileUploadCommand> fileData)
//        {
//            try
//            {
//                foreach (FileUploadModel file in fileData)
//                {
//                    var fileDetails = new FileDetails()
//                    {
//                        ID = 0,
//                        FileName = file.FileDetails.FileName,
//                        FileType = file.FileType,
//                    };

//                    using (var stream = new MemoryStream())
//                    {
//                        file.FileDetails.CopyTo(stream);
//                        fileDetails.FileData = stream.ToArray();
//                    }

//                    var result = dbContextClass.FileDetails.Add(fileDetails);
//                }
//                await dbContextClass.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task DownloadFileById(int Id)
//        {
//            try
//            {
//                var file = dbContextClass.FileDetails.Where(x => x.ID == Id).FirstOrDefaultAsync();

//                var content = new System.IO.MemoryStream(file.Result.FileData);
//                var path = Path.Combine(
//                   Directory.GetCurrentDirectory(), "FileDownloaded",
//                   file.Result.FileName);

//                await CopyStream(content, path);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task CopyStream(Stream stream, string downloadPath)
//        {
//            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
//            {
//                await stream.CopyToAsync(fileStream);
//            }
//        }
//    }
//}
