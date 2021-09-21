using HepsiYemek.DataService.Interfaces;
using HepsiYemek.DataService.UnitOfWork;
using HepsiYemek.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.DataService.Repositories
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IMongoDatabase client)
        {
            _documentDBRepository = new DocumentDBRepository<Product>(client);
        }
    }
}
