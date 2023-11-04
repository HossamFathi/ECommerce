using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Categories.Helper
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> getAll(int index, int size);
        Task<CategoryDTO> get(int CategoryID);
        Task<bool> Update(int CategoryID, CategoryDTO categoryDTO);
        Task<bool> Delete(int CategoryID);
        Task Insert(CategoryDTO category);
    }
}
