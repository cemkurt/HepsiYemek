using HepsiYemek.DataService.UnitOfWork;
using HepsiYemek.Models;
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
    public abstract class BaseService<T> where T : class, IBaseModel
    {
        public IDocumentDBRepository<T> _documentDBRepository;

        public virtual async Task<T> GetById(ObjectId Id)
        {
            var result = await _documentDBRepository.GetByIdAsync(Id);

            return result;
        }

        public virtual async Task<T> Add(T item)
        {

            

            var model = await _documentDBRepository.AddAsync(item);

            return model;
        }

        public virtual async Task<List<T>> AddManyAsycn(List<T> item)
        {
            var model = await _documentDBRepository.AddManyAsync(item);

            return model;
        }

        public virtual async Task<T> AddOrUpdate(T item)
        {
            if (string.IsNullOrEmpty(item.Id.ToString()) || item.Id.ToString().IndexOf("000000000000000000000000") != -1)
            {
                var model = await _documentDBRepository.AddAsync(item);
                return model;
            }
            else
            {
                var model = await _documentDBRepository.UpdateAsync(item.Id.ToString(), item);

                return model;
            }
        }

        public async Task<long> GetCountTotal(Expression<Func<T, bool>> predicate = null)
        {
            var don = await _documentDBRepository.GetCountTotal(predicate);

            return don;
        }

        public virtual async Task<T> Update(string id, T item)
        {
            var model = await _documentDBRepository.UpdateAsync(id, item);

            return model;
        }



        public virtual List<T> GetListQueryable(Expression<Func<T, bool>> predicate = null, int? take = null)
        {
            var model = _documentDBRepository.GetListQueryable(predicate, take);
            return model;
        }

        


        public virtual async Task<bool> Delete(string id)
        {
            await _documentDBRepository.DeleteAsync(id);

            return true;
        }

    }
}
