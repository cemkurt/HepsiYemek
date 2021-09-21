using HepsiYemek.DataService.Interfaces;
using HepsiYemek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private IRedisCacheClient _redisCacheClient;

        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IRedisCacheClient redisCacheClient, IProductService productService)
        {
            _redisCacheClient = redisCacheClient;
            _productService = productService;
            _logger = logger;
        }

        [HttpGet, Route("products")]
        public async Task<List<Product>> Products()
        {
            //  var categories = _categoryService.GetListQueryable(x => true);
            await _productService.AddOrUpdate(new Product { Name = "Pilav", Currency = "TL", Description = "leziz pilav", Price = 15 });

            var models = _productService.GetListQueryable(x => true);


            bool isAdded = await _redisCacheClient.Db0.AddAsync("Product", models, DateTimeOffset.Now.AddMinutes(3));

        //    var productdata = await _redisCacheClient.Db0.GetAllAsync<Product>;


            return models.ToList();
        }

    }
}
