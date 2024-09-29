namespace Data.Entities
{
    public class Product
    {
        //[MaxLength(200)]
        //[Range(0, 100)]
        //[EmailAddress]

        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        // ---- navigation properties
        public Category? Category { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
