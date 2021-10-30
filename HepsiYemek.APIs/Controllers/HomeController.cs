using HepsiYemek.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HepsiYemek.APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController  : ControllerBase
    {
        
        [HttpGet, Route("list")]
        [SwaggerOperation(Summary = "Product listesini verir, name parametresinde arama yapar", Description = "")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public IActionResult Index()
        {

            return Content("siri");
        }
    }
}