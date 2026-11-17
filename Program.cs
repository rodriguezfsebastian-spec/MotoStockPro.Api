using Microsoft.EntityFrameworkCore;
using MotoStockPro.Api.Data;
using MotoStockPro.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IInventarioService, InventarioService>();

var app = builder.Build();

app.UseMiddleware<MotoStockPro.Api.Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Habilita la interfaz visual interactiva
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("MotoStockPro API")
            .WithTheme(ScalarTheme.Mars);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();