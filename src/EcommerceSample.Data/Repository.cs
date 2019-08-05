using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EcommerceSample.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSample.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EcommerceContext _context;
        public Repository(EcommerceContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public T Get(Expression<Func<T, bool>> query)
        {
            var entity = _context.Set<T>().FirstOrDefault(query);
            return entity;
        }
        /// <inheritdoc />
        public bool IsExist(Expression<Func<T, bool>> query)
        {
            return _context.Set<T>().Any(query);
        }
        /// <inheritdoc />
        public T Add(T entity)
        {
            var entry = _context.Set<T>().Add(entity);
            return entry.Entity;
        }
        /// <inheritdoc />
        public T Update(T entity)
        {
            var entityToUpdate = _context.Attach(entity);
            entityToUpdate.State = EntityState.Modified;
            return entityToUpdate.Entity;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> query)
        {
            var list = _context.Set<T>().Where(query);
            return list.ToList();
        }
    }
}
