using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryanikyTaskV1.Converters;
using PryanikyTaskV1.Data;
using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        private readonly int _ordersOnPage = 3;

        public OrderController(IRepository repository, ILogger<OrderController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders(int page = 1) 
        {
            if (page < 1) return BadRequest(new { Error = "Invalid page number" });

            var result = await _repository.GetAll<Order>().Include(o => o.Product).Skip(_ordersOnPage * (page - 1))
                .Take(_ordersOnPage).Select(o => ViewModelConverter.ConvertOrderToViewModel(o)).ToArrayAsync();

            return Ok(result);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody]OrderForm model)
        {
            try
            {
                var product = await _repository.GetByIdAsync<Product>(model.ProductId);

                if (product == null)
                {
                    _logger.LogWarning($"Try to get product with a non-existent id: {model.ProductId}", DateTime.Now.ToLongTimeString());
                    return BadRequest(new { Error = $"There is no product with id: {model.ProductId}" });
                }

                var order = new Order() { Address = model.Address, CustomerName = model.CustomerName, Product = product };

                await _repository.AddItemAsync(order);
                return Ok();
            }
            catch(Exception ex) 
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
                await _repository.RemoveByIdAsync<Order>(id);
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
