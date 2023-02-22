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
        private readonly ILogger _logger;

        private readonly int _productsOnPage = 6;

        public ProductController(IRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(int page = 1)
        {
            if (page < 1) return BadRequest(new { Error = "Invalid page number" });

            var result = await _repository.GetAll<Product>().Skip(_productsOnPage * (page - 1))
                .Take(_productsOnPage).ToArrayAsync();

            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductForm model)
        {
            try
            {
                await _repository.AddItemAsync<Product>(new Product { Name = model.Name, Price = model.Price });
                return Ok();
            } catch (Exception ex) 
            {
                _logger.LogError(ex.Message, DateTime.Now.ToLongTimeString());
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _repository.RemoveByIdAsync<Product>(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, DateTime.Now.ToLongTimeString());
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

}
