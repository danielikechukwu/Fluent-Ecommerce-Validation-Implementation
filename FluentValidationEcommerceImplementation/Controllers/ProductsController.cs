using FluentValidation;
using FluentValidationEcommerceImplementation.Data;
using FluentValidationEcommerceImplementation.DTOs;
using FluentValidationEcommerceImplementation.Models;
using FluentValidationEcommerceImplementation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationEcommerceImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ECommerceDbContext _context;
        // private readonly IValidator<ProductDTO> _validator;

        public ProductsController(ILogger<ProductsController> logger, ECommerceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // POST: api/products
        // Creates a new product after validating the input data.
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDTO productDTO)
        {

            // Instantiate the validator with the DbContext for performing async validations.
            var validator = new ProductDTOValidator(_context);

            // Validate the request using the injected validator
            var validationResult = await validator.ValidateAsync(productDTO);

            // If validation fails, return a 400 Bad Request with error details.
            if(!validationResult.IsValid)
            {
                var errorResponse = validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return BadRequest(new { Errors = errorResponse });
            }

            // Map the validated DTO to a Product entity.
            var product = new Product
            {
                Name = productDTO.Name,
                SKU = productDTO.SKU,
                Price = productDTO.Price,
                CategoryId = productDTO.CategoryId,
                Stock = productDTO.Stock,
                Description = productDTO.Description,
                Discount = productDTO.Discount
            };

            // Add the new product to the database context.
            _context.Products.Add(product);

            // Save changes asynchronously.
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
