using AutoMapper;
using DataBaseLayer.models;
using DTO;
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
            #region Category

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<RelatedWork, RelatedWorkDTO>().ReverseMap();
            CreateMap<Setting, SettingDTO>().ReverseMap();
   
            #endregion

     


        }

    }
}
