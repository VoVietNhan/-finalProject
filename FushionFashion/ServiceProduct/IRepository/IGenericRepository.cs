using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BusinessObject.Entities;

namespace ServiceProduct.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid? id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Approve(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void SoftRemove(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void SoftRemoveRange(List<TEntity> entities);
    }
}
