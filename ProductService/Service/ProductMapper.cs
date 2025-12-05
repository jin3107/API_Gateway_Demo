using ProductService.DTO;
using ProductService.Models;

namespace ProductService.Service
{
    public static class ProductMapper
    {
        public static Product ToEntity(ProductRequest request)
        {
            return new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };
        }

        public static ProductResponse ToResponse(Product entity)
        {
            return new ProductResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CategoryId = entity.CategoryId
            };
        }
    }
}
