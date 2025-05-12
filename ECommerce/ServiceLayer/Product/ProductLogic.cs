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
using DTO.Entities.Product;
using DTO.Entities.Photo;
using Threenine.Data.Paging;
using DTO.Enums;

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

        public async Task<ProductDTO> get(LanguageCode code,int ProductID)
        {

          Product product = await _products.SingleOrDefaultAsync(pr => pr.Id == ProductID);
            if (product == null)
                return null;
            List<PhotoDTO> photos = (await _photo.getAll(ProductID)).ToList(); // include
         
          
            return code == LanguageCode.en ?  ConvertToProductDto(product) : ConvertToProductDtoArabic(product);
        }
        public async Task<AddProductDTO> get(int ProductID)
        {

            Product product = await _products.SingleOrDefaultAsync(pr => pr.Id == ProductID);
            if (product == null)
                return null;
            List<PhotoDTO> photos = (await _photo.getAll(ProductID)).ToList(); // include


            return ConvertToAddProductDto(product);
        }
        public async Task<IPaginate<ProductDTO>> getAll(LanguageCode code = LanguageCode.en,int index = 0, int size= 20)
        {
            IPaginate<Product> products = await _products.GetAll(index:index, size:size);
            
            return code == LanguageCode.en ?  Paginate.From(products, ConvertToProductDto) : Paginate.From(products, ConvertToProductDtoArabic);
            
             
        }
        public async Task<IPaginate<AddProductDTO>> getAll(int index = 0, int size = 20)
        {
            IPaginate<Product> products = await _products.GetAll(index: index, size: size);
            return Paginate.From(products, ConvertToAddProductDto);
        }

        public async Task Insert(AddProductDTO productDTO)
        {
            Product product = ConvertToProduct(productDTO);
           await _products.InsertEntityAsync(product);
        }
        public async Task<bool> Update(int ProductID, AddProductDTO productDTO)
        {
          Product product = await  _products.SingleOrDefaultAsync(pr => pr.Id == productDTO.Id);
            if (product == null)
                 return false;
            _mapper.Map(productDTO, product, typeof(ProductDTO) , typeof(Product));
            return await _products.update(product);

            

        }
        public async Task<IPaginate<ProductDTO>> getAll(LanguageCode code ,int CategoryID)
        {
            IPaginate<Product> products = await _products.GetAll( pro => pro.CategoryID == CategoryID);
            return code == LanguageCode.en ? Paginate.From(products, ConvertToProductDto) : Paginate.From(products, ConvertToProductDtoArabic);


        }


        #region help 
        private ProductDTO ConvertToProductDto(Product product)
        {
            return _mapper.Map<ProductDTO>(product);
        }
      
        private AddProductDTO ConvertToAddProductDto(Product product)
        {
            return _mapper.Map<AddProductDTO>(product);
        }
        private ProductDTO ConvertToProductDtoArabic(Product product)
        {
            var productMapped = _mapper.Map<ProductDTO>(product);
            productMapped.Describtion = product.ArabicDescribtion;
            productMapped.Name = product.ArabicName;
            return productMapped;
        }
        private IEnumerable<ProductDTO> ConvertToProductDto(IEnumerable<Product> products)
        {
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            foreach (var product in products)
            {
                productsDTO.Add(ConvertToProductDto(product));
            }
            return productsDTO;
        }
        private IEnumerable<ProductDTO> ConvertToProductDtoArabic(IEnumerable<Product> products)
        {
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            foreach (var product in products)
            {
                productsDTO.Add(ConvertToProductDtoArabic(product));
            }
            return productsDTO;
        }
        private IEnumerable<AddProductDTO> ConvertToAddProductDto(IEnumerable<Product> products)
        {
            List<AddProductDTO> productsDTO = new List<AddProductDTO>();
            foreach (var product in products)
            {
                productsDTO.Add(ConvertToAddProductDto(product));
            }
            return productsDTO;
        }
        private Product ConvertToProduct(ProductDTO productDTO)
        {

            return _mapper.Map<Product>(productDTO);
        }
        private Product ConvertToProduct(AddProductDTO productDTO)
        {

            return _mapper.Map<Product>(productDTO);
        }

        public async Task SetMainPhoto(int ProductID, string ImageURL)
        {
          Product product =   await _products.SingleOrDefaultAsync(prod => prod.Id == ProductID);
            product.ImageURL = ImageURL;
          await _products.update(product);
            return;
        }
        #endregion
    }
}
