using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BusinessObject.Dtos.Product;

namespace ServiceProduct.IServices
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetProduct();
        public Task<CreateProductViewModel?> CreateProduct(CreateProductViewModel productDTO);
        public Task<ProductViewModel> GetEnableProduct();
        public Task<ProductViewModel> GetDisableProduct();
        public Task<UpdateProductViewModel?> UpdateProduct(Guid id, UpdateProductViewModel productDTO);
        Task<ProductViewModel> GetProductById(Guid id);
    }
}
