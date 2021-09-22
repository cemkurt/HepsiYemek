using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.DataService.UnitOfWork
{
    public interface IDocumentDBRepository<T> where T : class
    {
        Task<T> AddAsync(T item);
        Task<List<T>> AddManyAsync(List<T> item);
        Task<T> UpdateAsync(string id, T model);
        List<T> GetListQueryable(Expression<Func<T, bool>> predicate =null, int? take = null);
        Task<IChangeStreamCursor<ChangeStreamDocument<T>>> WatchGetListAsync( );
        Task<T> GetByIdAsync(ObjectId Id);

        Task<bool>  DeleteAsync(string id);
        Task<long> GetCountTotal(Expression<Func<T, bool>> predicate = null);
    }
}
