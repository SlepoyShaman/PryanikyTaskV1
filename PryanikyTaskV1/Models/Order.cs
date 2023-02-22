namespace PryanikyTaskV1.Models
{
    public class Order : IWithId
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
