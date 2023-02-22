namespace PryanikyTaskV1.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
