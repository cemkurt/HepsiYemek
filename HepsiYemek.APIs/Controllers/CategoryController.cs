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
    public class CategoryController : ControllerBase
    {

        private IRedisCacheClient _redisCacheClient;

        private readonly ILogger<CategoryController> _logger;

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ILogger<CategoryController> logger, IMapper mapper, IRedisCacheClient redisCacheClient, ICategoryService categoryService)
        {
            _redisCacheClient = redisCacheClient;
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet, Route("list")]
        [SwaggerOperation(Summary = "Kateogir listesini verir, name parametresinde arama yapar", Description = "")]
        [ProducesResponseType(typeof(List<CategoryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<List<CategoryResponseDto>> CategoryList(string name)
        {
            var predicate = PredicateBuilder.New<Category>();

            predicate.And(x => true);

            if (!string.IsNullOrEmpty(name))
            {
                predicate.And(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            var models = _categoryService.GetListQueryable(predicate);

            var mapping = _mapper.Map(models, new List<CategoryResponseDto>());

            return mapping;
        }

        [HttpGet, Route("category/{id}")]
        [SwaggerOperation(Summary = "Kategori id bilgisine detayını verir", Description = "")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<CategoryResponseDto> CategoryDetail(string id)
        {

            var model = await _categoryService.GetById(ObjectId.Parse(id));

            var mapping = _mapper.Map(model, new CategoryResponseDto());

            return mapping;
        }

        [HttpPost, Route("category")]
        public async Task<Category> Category(Category requestDto)
        {

            await _categoryService.AddOrUpdate(requestDto);


            return requestDto;
        }

        [HttpPut, Route("category")]
        public async Task<Category> CategoryPut(CategoryResponseDto requestDto)
        {
            var model = await _categoryService.GetById(ObjectId.Parse(requestDto.Id));

            var map = _mapper.Map(requestDto, model);

            await _categoryService.AddOrUpdate(map);


            return map;
        }

        [HttpDelete, Route("category/{id}")]
        public async Task<IActionResult> CategoryDelete(string id)
        {

            try
            {
                var model = await _categoryService.GetById(ObjectId.Parse(id));
                if (model != null)
                {
                    _categoryService.Delete(id).Wait();
                    return Ok(new SuccessMessageResponse { Message = "Kaydınız silindi." });
                }
                else
                {
                    return BadRequest(new SuccessMessageResponse { Message = "Kayıt bulunamadı " });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(new SuccessMessageResponse { Message = "Hata meydana geldi" + ex.Message});

            }
        }
    }
}
