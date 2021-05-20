using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly IUnitOfWork _unitofwork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public TEntity GetFirstOrDefult(TId id)
        {
            return _unitofwork._context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return _unitofwork._context.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _unitofwork._context.Set<TEntity>();
        }

        public EntityEntry<TEntity> Attach(TEntity entity)
        {
           return _unitofwork._context.Set<TEntity>().Add(entity);
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            return _unitofwork._context.Set<TEntity>().Update(entity);
        }
        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            TEntity exist = _unitofwork._context.Set<TEntity>().Find(entity);
            if(exist != null)
            {
                return _unitofwork._context.Set<TEntity>().Remove(exist);
            }
            else
            {
                return null;
            }
        }
    }
}
