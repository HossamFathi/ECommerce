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
using DTO.Entities.Photo;
using Threenine.Data.Paging;
using ServiceLayer.Products.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLayer.Photos
{
    internal class PhotoLogic : IPhotoService
    {
        private readonly IRepository<Photo> _Photo;
        private readonly IMapper _mapper;
        private IProductService _products;
        private IServiceProvider _serviceProvider;


        public PhotoLogic(IRepository<Photo> photo, IMapper mapper, IFileImageUploading upload, IServiceProvider serviceProvider)
        {
            _Photo = photo;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }
        public async Task<bool> Delete(int PhotoID)
        {
           return await _Photo.Delete(PhotoID);
        }


        public async Task<IEnumerable<PhotoDTO>> getAll(int productID)
        {
           IPaginate<Photo>  photos= await _Photo.GetAll(ph => ph.ProductID == productID);
            return photos.Items.Select(ConvertToPhotoDto);
        }

        private PhotoDTO ConvertToPhotoDto(Photo photo)
        {
           return _mapper.Map<PhotoDTO>(photo);
        }

        public async Task Insert(AddPhotoDTO photoDTO)
        {
            _products = _serviceProvider.GetRequiredService<IProductService>();

            Photo Photo = ConvertToPhoto(photoDTO);
            Photo.path = photoDTO.GetPath();
            if (photoDTO.IsDefault)
            {
                await _products.SetMainPhoto(photoDTO.ProductID, Photo.path);
            }
            await _Photo.InsertEntityAsync(Photo);

        }

        public async Task SetDefault(int photoID)
        {
            _products = _serviceProvider.GetRequiredService<IProductService>();

           Photo photo = await _Photo.SingleOrDefaultAsync(ph => ph.ID == photoID);
            if (photo == null)
                return;
           await _products.SetMainPhoto(photo.ProductID, photo.path);
            
        }

        private Photo ConvertToPhoto(PhotoDTO photoDTO)
        {
           return _mapper.Map<Photo>(photoDTO);
        }
        private Photo ConvertToPhoto(AddPhotoDTO photoDTO)
        {
            return _mapper.Map<Photo>(photoDTO);
        }


    }
}
