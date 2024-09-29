namespace Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
