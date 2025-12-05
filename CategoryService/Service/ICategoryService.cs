using CategoryService.DTO;

namespace CategoryService.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> GetByIdAsync(Guid id);
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
        Task<CategoryResponse> UpdateAsync(CategoryRequest request);
        Task<string> DeleteAsync(Guid id);
    }
}
