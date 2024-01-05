
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Categories;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Identity.Helper;
using ServiceLayer.Identity;
using ServiceLayer.Photos;
using ServiceLayer.Photos.Helper;
using ServiceLayer.Products;
using ServiceLayer.Products.Helper;
using ServiceLayer.RelatedWorks;
using ServiceLayer.RelatedWorks.Helper;
using ServiceLayer.Settings;
using ServiceLayer.Settings.Helper;
using ServiceLayer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Message.Helper;
using ServiceLayer.Message;

namespace DTO
{
    public static class ServiceCollectionExtensions
    {

        public static void RegisterYourServices(this IServiceCollection services)
        {

            // Auto Mapper Configurations
            #region Auto Mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddTransient<ICategoryService,CategoryLogic>();
            services.AddTransient<IProductService,ProductLogic>();
            services.AddTransient<IPhotoService,PhotoLogic>();
            services.AddTransient<ISettingService, SettingLogic>();
            services.AddTransient<IRelatedWorkSerivce, RelatedWorkLogic>();
            services.AddTransient<IFileImageUploading, FileImage>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IMessage, MessageLogic>();

        }
    }

}
