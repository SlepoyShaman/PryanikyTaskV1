using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Converters
{
    public static class ViewModelConverter
    {
        public static ProductViewModel ConvertProductToViewModel(Product product)
            => new ProductViewModel() { Id = product.Id, Name = product.Name, Price= product.Price };

        public static OrderViewModel ConvertOrderToViewModel(Order order)
            => new OrderViewModel() 
            { 
                Id = order.Id, 
                Address= order.Address, 
                CustomerName = order.CustomerName,
                Product = ConvertProductToViewModel(order.Product)
            };
    }
}
