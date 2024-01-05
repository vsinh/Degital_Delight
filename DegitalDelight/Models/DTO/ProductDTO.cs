namespace DegitalDelight.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; } = 0;
        public string? Brand { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? ProductTypeId { get; set; }
        public int? WarrantyId { get; set; }
        public List<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
    }
}