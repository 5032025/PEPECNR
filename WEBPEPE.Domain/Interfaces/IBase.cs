using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WEBPEPE.Domain.Entities;

namespace WEBPEPE.Domain.Interfaces
{
    public interface IBase<TEntity> where TEntity : BaseEntity
    {

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] incluides);

        Task<bool> Delete(int id);

        Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, int skip, int take, string search);
    }
}
