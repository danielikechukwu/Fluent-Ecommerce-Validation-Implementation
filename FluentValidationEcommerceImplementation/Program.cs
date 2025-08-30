using FluentValidationEcommerceImplementation.Data;
using FluentValidationEcommerceImplementation.DTOs;
using FluentValidationEcommerceImplementation.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Keep original property names during serialization/deserialization.
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Register the ECommerceDbContext with dependency injection
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FluentValidationEcommerceDBConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Register Each Validator Manually
builder.Services.AddScoped<IValidator<ProductDTO>, ProductDTOValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
