using DTO.Entities.Category;
using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

namespace ServiceLayer.Categories.Helper
{
    public interface ICategoryService
    {
        Task<IPaginate<CategoryDTO>> getAll(LanguageCode code = LanguageCode.en, int index = 0, int size = 20);
        Task<CategoryDTO> get(LanguageCode code, int CategoryID);
        Task<bool> Update(int CategoryID, AddCategoryDTO categoryDTO);
        Task<bool> Delete(int CategoryID);
        Task Insert(AddCategoryDTO category);
        Task<IPaginate<AddCategoryDTO>> getAll(int index = 0, int size = 20);
        Task<AddCategoryDTO> get(int CategoryID);
    }
}
