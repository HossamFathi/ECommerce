using AutoMapper;
using DataBaseLayer;
using DataBaseLayer.models;
using DTO;
using ServiceLayer.Categories.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<CategoryDTO> get(int CategoryID)
        {
           Category category =  await _Category.singleOrDefault(ca => ca.ID == CategoryID);
           return ConvertToCategoryDTO(category);
        }

        private CategoryDTO ConvertToCategoryDTO(Category category)
        {
          return  _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> getAll(int index, int size)
        {
            var Categories = await _Category.getAll();
            return Categories.Select(ConvertToCategoryDTO);
        }

        public async Task Insert(CategoryDTO categoryDTo)
        {
            Category category = ConvertToCategory(categoryDTo);
             await _Category.InsertEntityAsync(category);
        }

        private Category ConvertToCategory(CategoryDTO categoryDTo)
        {
        return  _mapper.Map<Category>(categoryDTo);
        }

        public Task<bool> Update(int CategoryID, CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
