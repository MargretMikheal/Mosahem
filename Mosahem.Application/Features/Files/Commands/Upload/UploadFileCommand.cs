using MediatR;
using Microsoft.AspNetCore.Http;
using mosahem.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosahem.Application.Features.Files.Commands.Upload
{
    public class UploadFileCommand : IRequest<Response<string>>
    {
        public IFormFile File { get; set; }
        public string FolderName { get; set; } 
    }
}
