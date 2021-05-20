using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : BaseEntity<TId> where TId : class;
        DbContext _context { get; }
        int Commit();
    }

    //public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    //{
    //    TContext _context { get; }
    //}
}
