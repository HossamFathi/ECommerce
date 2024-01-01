using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTO.Entities.Photo
{
    public interface IFileImage
    {
        public IFormFile ImageOrFile { get; set; }
    }
}
