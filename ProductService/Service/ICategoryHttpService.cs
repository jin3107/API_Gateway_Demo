namespace ProductService.Service
{
    public interface ICategoryHttpService
    {
        Task<string?> GetCategoryNameByIdAsync(Guid categoryId);
    }
}
