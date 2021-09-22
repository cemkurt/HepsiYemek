using AutoMapper;
using HepsiYemek.DataService.Interfaces;
using HepsiYemek.Models;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
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

        private readonly ICategoryService  _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(ILogger<ProductController> logger, IMapper mapper, IRedisCacheClient redisCacheClient, IProductService productService,
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _redisCacheClient = redisCacheClient;
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet, Route("list")]
        [SwaggerOperation(Summary = "Product listesini verir, name parametresinde arama yapar", Description = "")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<List<ProductResponseDto>> ProductList(string name)
        {
            var predicate = PredicateBuilder.New<Product>();

            predicate.And(x => true);

            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            var models = _productService.GetListQueryable(predicate);

            var returnList = new List<ProductResponseDto>();
            if (models.Any())
            {
                returnList = _mapper.Map<List<ProductResponseDto>>(models);
                
            }

            return returnList;
        }

        [HttpGet, Route("products/{id}")]
        [SwaggerOperation(Summary = "Product id bilgisine detayını verir", Description = "")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ProductResponseDto> ProductDetail(string id)
        {
            



            var productdata = await _redisCacheClient.Db0.GetAsync<ProductResponseDto>("Product_" + id);
            if (productdata == null)
            {
                var product = await _productService.GetById(ObjectId.Parse(id));
                var mapped = _mapper.Map<ProductResponseDto>(product);

                productdata = mapped;

                bool isAdded = await _redisCacheClient.Db0.AddAsync("Product_" + id, mapped, DateTimeOffset.Now.AddMinutes(5));
            }


            return productdata;
        }

        [HttpPost, Route("products")]
        public async Task<ProductResponseDto> Products(ProductRequestDto requestDto)
        {

            var category = await _categoryService.GetById(ObjectId.Parse(requestDto.CategoryId));


            var map = _mapper.Map<Product>(requestDto);
            map.CategoryId = _mapper.Map(category, new CategoryResponseDto());

            await _productService.AddOrUpdate(map);

            var model = await _productService.GetById(map.Id);




            var response = _mapper.Map<ProductResponseDto>(model);

            return response;
        }

        [HttpPut, Route("products/{id}")]
        public async Task<ProductResponseDto> ProductPut(string id,ProductRequestDto requestDto)
        {
            var model = await _productService.GetById(ObjectId.Parse(id));

            var map = _mapper.Map(requestDto, model);
            map.Id = ObjectId.Parse(id);

            await _productService.AddOrUpdate(map);

            var response = _mapper.Map<ProductResponseDto>(map);

            return response;
        }

        [HttpDelete, Route("products/{id}")]
        public async Task<IActionResult> ProductDelete(string id)
        {

            try
            {
                var model = await _productService.GetById(ObjectId.Parse(id));
                if (model != null)
                {
                    _productService.Delete(id).Wait();
                    return Ok(new SuccessMessageResponse { Message = "Kaydınız silindi." });
                }
                else
                {
                    return BadRequest(new SuccessMessageResponse { Message = "Kayıt bulunamadı " });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(new SuccessMessageResponse { Message = "Hata meydana geldi" + ex.Message });

            }
        }
    }
}
