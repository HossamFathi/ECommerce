using DTO;
using DTO.Entities.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Photos.Helper
{
    public interface IPhotoService
    {

        Task<IEnumerable<PhotoDTO>> getAll(int productID);
        Task<bool> Delete(int PhotoID);
        Task Insert(AddPhotoDTO photoDTO);
        Task SetDefault(int photoID);
    }
}
