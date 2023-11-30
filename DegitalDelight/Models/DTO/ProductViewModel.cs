namespace DegitalDelight.Models.DTO
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; } // Add this property to display product type name
    }
}
