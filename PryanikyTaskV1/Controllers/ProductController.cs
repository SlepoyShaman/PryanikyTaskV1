using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryanikyTaskV1.Data;
using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly int _productsOnPage = 6;
        private readonly ILogger _logger;

        public ProductController(IRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(int page = 1)
        {
            if (page < 1) return BadRequest(new { Error = "Invalid page number" });

            var result = await _repository.GetAllProducts().Skip(_productsOnPage * (page - 1)).Take(_productsOnPage).ToArrayAsync();

            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductViewModel model)
        {
            try
            {
                await _repository.AddProduct(new Product { Name = model.Name, Price = model.Price });
                return Ok();
            } catch (Exception ex) 
            {
                _logger.LogError(ex.Message, DateTime.Now.ToLongTimeString());
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("MakeOrder/{id}")]
        public async Task<IActionResult> MakeOrder(int id)
        {
            try
            {
                await _repository.RemoveProductById(id);
                return Ok();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message, DateTime.Now.ToLongTimeString());
                return BadRequest(new { Error = ex.Message });
            }
        }



    }
}
