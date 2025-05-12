using DTO;
using DTO.Entities.Product;
using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threenine.Data.Paging;

namespace ServiceLayer.Products.Helper
{
    public interface IProductService
    {
        Task<IPaginate<ProductDTO>> getAll(LanguageCode code, int index, int size);
        Task<IPaginate<ProductDTO>> getAll(LanguageCode code, int CategoryID);
        Task<ProductDTO> get(LanguageCode code,int ProductID);
        Task<bool> Update(int ProductID, AddProductDTO productDTO);
        Task<bool> Delete(int ProductID);
        Task Insert(AddProductDTO productDTO);
        Task<IPaginate<AddProductDTO>> getAll(int index = 0, int size = 20);
        Task<AddProductDTO> get(int ProductID);
        Task SetMainPhoto(int ProductID , string ImageURL);
    }
}
