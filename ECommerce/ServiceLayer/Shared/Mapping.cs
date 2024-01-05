using AutoMapper;
using DataBaseLayer.models;
using DTO;
using DTO.Entities.Category;
using DTO.Entities.Message;
using DTO.Entities.Photo;
using DTO.Entities.Product;
using DTO.Entities.RelatedWork;
using DTO.Entities.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceLayer.Shared
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            #region map

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, AddCategoryDTO>().ReverseMap();
            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<Photo, AddPhotoDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, AddProductDTO>().ReverseMap();
            CreateMap<RelatedWork, RelatedWorkDTO>().ReverseMap();
            CreateMap<RelatedWork, AddRelatedWorkDTO>().ReverseMap();
            CreateMap<Setting, SettingDTO>().ReverseMap();
            CreateMap<Messages, MessageDTO>().ReverseMap();
   
            #endregion   

     


        }

    }
}
