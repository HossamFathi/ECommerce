using AutoMapper;
using DataBaseLayer.models;
using DataBaseLayer;
using DTO;
using ServiceLayer.Products.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Photos.Helper;

namespace ServiceLayer.Products
{
    public class ProductLogic : IProductService
    {
        private readonly IRepository<Product> _products;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photo;
        public ProductLogic(IRepository<Product> products, IMapper mapper, IPhotoService photo)
        {
            _products = products;
            _mapper = mapper;
            _photo = photo;
        }
        public async Task<bool> Delete(int ProductID)
        {
         return await  _products.Delete(ProductID);
        }

        public async Task<ProductDTO> get(int ProductID)
        {

          Product product = await _products.singleOrDefault(pr => pr.Id == ProductID);
            if (product == null)
                return null;
            IEnumerable<PhotoDTO> photos = await _photo.getAll(ProductID); // include
            ProductDTO productDTO = ConvertToProductDto(product);
            productDTO.InsertPhotos(photos);
            return productDTO;
        }

        private ProductDTO ConvertToProductDto(Product product)
        {
          return  _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> getAll(int index, int size)
        {
         IEnumerable<Product> products =  await  _products.getAll();
          return  products.Select(ConvertToProductDto);
        }

        public async Task Insert(ProductDTO productDTO)
        {
            Product product = ConvertToProduct(productDTO);
           await _products.InsertEntityAsync(product);
        }

        private Product ConvertToProduct(ProductDTO productDTO)
        {
            
            return   _mapper.Map<Product>(productDTO);
        }

        public Task<bool> Update(int ProductID, ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDTO>> getAll(int CategoryID)
        {
            IEnumerable<Product> products = await _products.getAll(pro => pro.CategoryID == CategoryID);
            return products.Select(ConvertToProductDto);

        }
    }
}
