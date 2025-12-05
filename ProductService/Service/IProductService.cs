using ProductService.DTO;

namespace ProductService.Service
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(Guid id);
        Task<List<ProductResponse>> GetByCategoryIdAsync(Guid categoryId);
        Task<ProductResponse> CreateAsync(ProductRequest request);
        Task<ProductResponse> UpdateAsync(ProductRequest request);
        Task<string> DeleteAsync(Guid id);
    }
}
