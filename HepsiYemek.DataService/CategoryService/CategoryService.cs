using HepsiYemek.DataService.Interfaces;
using HepsiYemek.DataService.UnitOfWork;
using HepsiYemek.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.DataService.Repositories
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IMongoDatabase client)
        {
            _documentDBRepository = new DocumentDBRepository<Category>(client);
        }
    }
}
