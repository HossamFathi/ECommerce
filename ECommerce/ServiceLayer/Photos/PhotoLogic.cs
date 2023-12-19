using AutoMapper;
using DataBaseLayer.models;
using DataBaseLayer;
using DTO;
using ServiceLayer.Photos.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Photos
{
    internal class PhotoLogic : IPhotoService
    {
        private readonly IRepository<Photo> _Photo;
        private readonly IMapper _mapper;
    
        public PhotoLogic(IRepository<Photo> photo, IMapper mapper, IFileImageUploading upload)
        {
            _Photo = photo;
            _mapper = mapper;
           
        }
        public async Task<bool> Delete(int PhotoID)
        {
           return await _Photo.Delete(PhotoID);
        }


        public async Task<IEnumerable<PhotoDTO>> getAll(int productID)
        {
           IEnumerable<Photo>  photos= await _Photo.getAll(ph => ph.ProductID == productID);
            return photos.Select(ConvertToPhotoDto);
        }

        private PhotoDTO ConvertToPhotoDto(Photo photo)
        {
           return _mapper.Map<PhotoDTO>(photo);
        }

        public async Task Insert(PhotoDTO photoDTO)
        {
           

                Photo Photo = ConvertToPhoto(photoDTO);
             
                await _Photo.InsertEntityAsync(Photo);
            
            
        }

        private Photo ConvertToPhoto(PhotoDTO photoDTO)
        {
           return _mapper.Map<Photo>(photoDTO);
        }

        
    }
}
