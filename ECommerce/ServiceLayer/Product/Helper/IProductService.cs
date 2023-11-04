using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Products.Helper
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> getAll(int index, int size);
        Task<IEnumerable<ProductDTO>> getAll(int CategoryID);
        Task<ProductDTO> get(int ProductID);
        Task<bool> Update(int ProductID, ProductDTO productDTO);
        Task<bool> Delete(int ProductID);
        Task Insert(ProductDTO productDTO);
    }
}
