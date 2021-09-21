using HepsiYemek.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HepsiYemek.DataService.UnitOfWork
{
    public class DocumentDBRepository<T> : IDocumentDBRepository<T> where T : class, IBaseModel
    {
        private readonly IMongoCollection<T> mongoCollection;

        public DocumentDBRepository(IMongoDatabase client)
        {
            mongoCollection = client.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> GetByIdAsync(string documentId)
        {
            var docId = new ObjectId(documentId);
            return await mongoCollection.Find<T>(m => m.Id == docId).FirstOrDefaultAsync();
        }

        public virtual List<T> GetListQueryable(Expression<Func<T, bool>> predicate = null, int? take = null)
        {
            Expression<Func<T, bool>> _predicate;
            if (predicate != null)
            {
                _predicate = predicate;
            }
            else
            {
                _predicate = x => true;
            }

            int rakam = 999;
            if (take != null)
            {
                rakam = take.Value;
            }

            return mongoCollection.AsQueryable<T>().Where(_predicate).Take(rakam).ToList();

        }

        public async Task<IChangeStreamCursor<ChangeStreamDocument<T>>> WatchGetListAsync()
        {


            return await mongoCollection.WatchAsync();


            using (var cursor = await mongoCollection.WatchAsync())
            {
                //foreach (var change in cursor.ToEnumerable())
                //{
                //    // process change event
                //}

                
            }


            //return mongoCollection.Watch<T>().ToList();

        }

        public async Task<long> GetCountTotal(Expression<Func<T, bool>> predicate = null)
        {
            Expression<Func<T, bool>> _predicate;
            if (predicate != null)
            {
                _predicate = predicate;
            }
            else
            {
                _predicate = x => true;
            }


            var don = await mongoCollection.CountAsync(_predicate);


            return don;
        }


        public async Task<T> AddAsync(T item)
        {
            await mongoCollection.InsertOneAsync(item);
            return item;
        }

        public async Task<List<T>> AddManyAsync(List<T> item)
        {
            await mongoCollection.InsertManyAsync(item);
            return item;
        }


        public virtual async Task<T> UpdateAsync(string id, T model)
        {
            var docId = new ObjectId(id);
            await mongoCollection.ReplaceOneAsync(m => m.Id == docId, model);

            return model;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var docId = new ObjectId(id);
            await mongoCollection.DeleteOneAsync(m => m.Id == docId);

            return true;
        }
    }
}
