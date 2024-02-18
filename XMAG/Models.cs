namespace XMAG
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public string? Uwagi { get; set; }
    }
}
