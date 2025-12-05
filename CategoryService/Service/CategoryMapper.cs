using CategoryService.DTO;
using CategoryService.Models;

namespace CategoryService.Service
{
    public static class CategoryMapper
    {
        public static Category ToEntity(CategoryRequest request)
        {
            return new Category
            {
                Name = request.Name,
                Description = request.Description
            };
        }

        public static CategoryResponse ToResponse(Category entity)
        {
            return new CategoryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
