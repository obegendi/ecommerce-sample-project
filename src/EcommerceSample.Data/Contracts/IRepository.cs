using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EcommerceSample.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        T Get(Expression<Func<T, bool>> query);
        bool IsExist(Expression<Func<T, bool>> query);
        T Add(T entity);
        T Update(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> query);
    }
}
