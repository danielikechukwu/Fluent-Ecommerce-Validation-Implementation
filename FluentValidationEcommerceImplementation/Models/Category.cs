namespace FluentValidationEcommerceImplementation.Models
{
    // Represents a product category (e.g., Electronics, Books, Home Appliances)
    public class Category
    {
        public int CategoryId { get; set; } // Primary Key

        public string CategoryName { get; set; } // Name of the category

        public string? Description { get; set; } // Optional description of the category

        // Navigation property: A category can have multiple products.
        public List<Product> Products { get; set; }
    }
}
