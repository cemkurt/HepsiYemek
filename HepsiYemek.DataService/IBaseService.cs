using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.DataService
{
    public interface IBaseService<T> where T : class
    {
        Task<T> GetById(ObjectId Id);
        Task<T> AddOrUpdate(T item);
        Task<T> Add(T item);
        Task<List<T>> AddManyAsycn(List<T> item);
        Task<T> Update(string id, T item);
        List<T> GetListQueryable(Expression<Func<T, bool>> predicate =null, int? take=null);
        Task<bool> Delete(string id);
        Task<long> GetCountTotal(Expression<Func<T, bool>> predicate = null);
    }
}
