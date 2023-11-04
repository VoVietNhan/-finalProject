using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.Size;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceProduct.IServices
{
    public interface ISizeService
    {
        Task<List<SizeViewModel>> GetSize();
        public Task<CreateSizeViewModel?> CreateSize(CreateSizeViewModel sizeDTO);
        Task DeleteSize(Guid id);
        public Task<UpdateSizeViewModel?> UpdateSize(Guid id, UpdateSizeViewModel sizeDTO);
    }
}
