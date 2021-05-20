using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<TEntity, TId>  where TEntity : BaseEntity<TId>
    {
        /// <summary>
        /// get first or default item based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetFirstOrDefult(TId id);
        /// <summary>
        /// get all items
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll();
        /// <summary>
        /// get all items that match the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Attach new item
        /// </summary>
        /// <param name="entity"></param>
        public EntityEntry<TEntity> Attach(TEntity entity);
        /// <summary>
        /// Update existing item
        /// </summary>
        /// <param name="entity"></param>
        public EntityEntry<TEntity> Update(TEntity entity);
        /// <summary>
        /// delete item
        /// </summary>
        /// <param name="entity"></param>
        public EntityEntry<TEntity> Delete(TEntity entity);
    }
}
