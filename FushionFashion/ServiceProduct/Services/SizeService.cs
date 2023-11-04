using AutoMapper;
using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.Size;
using BusinessObject.Entities.Product;
using Microsoft.AspNetCore.SignalR;
using ServiceProduct.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceProduct.Services
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public async Task<CreateSizeViewModel> CreateSize(CreateSizeViewModel sizeDTO)
        {
            var size = _mapper.Map<Size>(sizeDTO);
            await _unitOfWork.SizeRepository.AddAsync(size);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<CreateSizeViewModel>(size);
            }
            return null;
        }

        public async Task DeleteSize(Guid id)
        {
            var size = await _unitOfWork.SizeRepository.GetByIdAsync(id);
            _unitOfWork.SizeRepository.Update(size);
            _unitOfWork.SaveChangeAsync();

        }

        public async Task<List<SizeViewModel>> GetSize()
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllAsync();
            return _mapper.Map<List<SizeViewModel>>(sizes);
        }

        public async Task<UpdateSizeViewModel> UpdateSize(Guid id, UpdateSizeViewModel sizeDTO)
        {
            var size = await _unitOfWork.SizeRepository.GetByIdAsync(id);

            if (size == null)
            {
                return null;
            }
            _mapper.Map(sizeDTO, size);

            _unitOfWork.SizeRepository.Update(size);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

            if (isSuccess)
            {
                return _mapper.Map<UpdateSizeViewModel>(size);
            }

            return null;
        }
    }
}
