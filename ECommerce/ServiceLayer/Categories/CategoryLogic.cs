using AutoMapper;
using DataBaseLayer;
using DataBaseLayer.models;
using DTO.Entities.Category;
using DTO.Enums;
using ServiceLayer.Categories.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

namespace ServiceLayer.Categories
{
    internal class CategoryLogic : ICategoryService
    {
       private readonly IRepository<Category> _Category;
        private readonly IMapper _mapper;
        public CategoryLogic(IRepository<Category> category, IMapper mapper)
        {
            _Category = category;
            _mapper = mapper;
        }
        public async Task<bool> Delete(int CategoryID)
        {
          return await _Category.Delete(CategoryID);
        }

        public async Task<CategoryDTO> get(LanguageCode code , int CategoryID)
        {
            Category category = await _Category.SingleOrDefaultAsync(ca => ca.ID == CategoryID);
            if (category == null)
                return null;
            return code == LanguageCode.en ? 
                    ConvertToCategoryDTO(category) : 
                    ConvertToCategoryDTOArabic(category);
        }
        public async Task<AddCategoryDTO> get(int CategoryID)
        {
            Category category = await _Category.SingleOrDefaultAsync(ca => ca.ID == CategoryID);
            if (category == null)
                return null;
            return ConvertToAddCategoryDTO(category);
        }
        public async Task<IPaginate<CategoryDTO>> getAll(LanguageCode code = LanguageCode.en, int index = 0, int size = 20)
        {
            IPaginate<CategoryDTO> Categories = await _Category.GetAll(cat => code == LanguageCode.en ? 
                                                new CategoryDTO() 
                                                { ID = cat.ID , Photo = cat.Photo, Name = cat.Name , Describtion = cat.Describtion }: 
                                                new CategoryDTO() 
                                                { ID = cat.ID, Photo = cat.Photo, Name = cat.ArabicName, Describtion = cat.ArabicDescribtion }, 
                                                index:index, size:size
                                                );
            return Categories;
        }
        public async Task<IPaginate<AddCategoryDTO>> getAll( int index = 0, int size = 20)
        {
            IPaginate<Category> Categories = await _Category.GetAll(index: index, size: size);

            return Paginate.From(Categories,ConvertToAddCategoryDTO);
        }

        public async Task Insert(AddCategoryDTO categoryDTo)
        {
            Category category = ConvertToCategory(categoryDTo);
            category.Photo = categoryDTo.GetPhoto();
             await _Category.InsertEntityAsync(category);
        }

        public async Task<bool> Update(int CategoryID, AddCategoryDTO categoryDTO)
        {
            
            Category categoy = await _Category.SingleOrDefaultAsync(cat=>cat.ID == CategoryID);
            if (categoy == null) { return false; }
            _mapper.Map(categoryDTO, categoy, typeof(AddCategoryDTO), typeof(Category));
          return  await _Category.update(categoy);
         

        }


        private Category ConvertToCategory(CategoryDTO categoryDTo)
        {
            return _mapper.Map<Category>(categoryDTo);
        }
        private Category ConvertToCategory(AddCategoryDTO categoryDTo) { return _mapper.Map<Category>(categoryDTo); }
        private AddCategoryDTO ConvertToAddCategoryDTO(Category categoryDTo) { return _mapper.Map<AddCategoryDTO>(categoryDTo); }
        private IEnumerable<AddCategoryDTO> ConvertToAddCategoryDTO(IEnumerable<Category> category)
        {
            List<AddCategoryDTO> categories = new List<AddCategoryDTO>();
            foreach (var categoryItem in category) {

                categories.Add(ConvertToAddCategoryDTO(categoryItem));



            }
            return categories;
        }
        private CategoryDTO ConvertToCategoryDTO(Category category)
        {
            return _mapper.Map<CategoryDTO>(category);
        }
        private CategoryDTO ConvertToCategoryDTOArabic(Category category)
        {
            return new CategoryDTO
            {
                ID = category.ID,
                Describtion = category.ArabicDescribtion,
                Name = category.ArabicName,
                Photo = category.Photo,
            };

        }

    }
}
