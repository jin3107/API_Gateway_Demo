using CategoryService.DTO;
using CategoryService.Models;
using CategoryService.Repository;

namespace CategoryService.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository<Category> _categoryRepository;

        public CategoryService(ICategoryRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                return categories.Select(CategoryMapper.ToResponse).ToList();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<CategoryResponse> GetByIdAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new Exception($"Category với id {id} không tồn tại");

                return CategoryMapper.ToResponse(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            try
            {
                var category = CategoryMapper.ToEntity(request);
                category.Id = Guid.NewGuid();

                await _categoryRepository.AddAsync(category);

                return CategoryMapper.ToResponse(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<CategoryResponse> UpdateAsync(CategoryRequest request)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.Id);
                category.Name = request.Name;
                category.Description = request.Description;

                await _categoryRepository.UpdateAsync(category);

                return CategoryMapper.ToResponse(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new Exception($"Category với id {id} không tồn tại");

                await _categoryRepository.DeleteAsync(id);

                return $"Xóa category '{category.Name}' thành công";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }
    }
}
