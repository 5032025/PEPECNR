using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;

namespace WEBPEPE.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBase<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {

            _context = applicationDbContext;

        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {

            await _context.Set<TEntity>().AddAsync(entity);


            var rowsAffected = await _context.SaveChangesAsync();

            if (rowsAffected > 0)
            {
                return entity;
            }


            return null;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null) return false;

            entity.IsDeleted = true;

            return await _context.SaveChangesAsync() >= 1;
        }

        public virtual async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public virtual async Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] incluides)
        {

            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var include in incluides)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, int take, int skip, string search)
        {
            var query = _context.Set<TEntity>().Where(predicate);


            //Offset = Skip, Top = Take
            return query.Skip(skip).Take(take).AsQueryable();
        }
    }
}
