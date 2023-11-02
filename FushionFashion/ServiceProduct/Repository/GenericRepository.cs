using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceProduct.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ServiceProduct.IServices;

namespace ServiceProduct.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private ICurrentTime _timeService;
        private IClaimService _claimService;


        public GenericRepository(DBContext appDBContext,
            ICurrentTime currentTime,
            IClaimService claimService) // contructor 3 param
        {
            _dbSet = appDBContext.Set<TEntity>();
            _timeService = currentTime;
            _claimService = claimService;
        }

        public async Task AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _claimService.GetCurrentUserId;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy = _claimService.GetCurrentUserId;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public Task<List<TEntity>> GetAllAsync() => _dbSet.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Guid? id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public void SoftRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteDate = _timeService.CurrentTime();
            entity.DeleteBy = _claimService.GetCurrentUserId;
            _dbSet.Update(entity);
        }

        public void SoftRemoveRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeleteDate = _timeService.CurrentTime();
                entity.DeleteBy = _claimService.GetCurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedDate = _timeService.CurrentTime();
            entity.ModifiedBy = _claimService.GetCurrentUserId;
            _dbSet.Update(entity);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy = _claimService.GetCurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }
        public void Approve(TEntity entity)
        {
            entity.ApprovedDate = _timeService.CurrentTime();
            entity.ApprovedBy = _claimService.GetCurrentUserId;
            _dbSet.Update(entity);
        }
    }
}

