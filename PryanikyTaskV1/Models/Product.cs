namespace PryanikyTaskV1.Models
{
    public class Product : IWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<Order> Orders { get; set; }
    }
}
