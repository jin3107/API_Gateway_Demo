using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repository;
using ProductService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 44))
    )
);

// Register Generic Repository
builder.Services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));

// Register ProductService
builder.Services.AddScoped<IProductService, ProductService.Service.ProductService>();

// Register HttpClient for CategoryService communication
builder.Services.AddHttpClient<ICategoryHttpService, CategoryHttpService>(client =>
{
    var categoryServiceUrl = builder.Configuration["CategoryServiceUrl"] ?? "http://categoryservice:8080";
    client.BaseAddress = new Uri(categoryServiceUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Auto-migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
