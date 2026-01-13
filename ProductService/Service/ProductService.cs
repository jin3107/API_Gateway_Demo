using ProductService.DTO;
using ProductService.Models;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository<Product> _productRepository;
        private readonly ICategoryHttpService _categoryHttpService;

        public ProductService(IProductRepository<Product> productRepository, ICategoryHttpService categoryHttpService)
        {
            _productRepository = productRepository;
            _categoryHttpService = categoryHttpService;
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                var responses = new List<ProductResponse>();

                foreach (var product in products)
                {
                    var response = ProductMapper.ToResponse(product);
                    response.CategoryName = await _categoryHttpService.GetCategoryNameByIdAsync(product.CategoryId);
                    responses.Add(response);
                }

                return responses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<ProductResponse> GetByIdAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    throw new Exception($"Product với id {id} không tồn tại");

                var response = ProductMapper.ToResponse(product);
                response.CategoryName = await _categoryHttpService.GetCategoryNameByIdAsync(product.CategoryId);


                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<List<ProductResponse>> GetByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                var categoryProducts = products.Where(p => p.CategoryId == categoryId).ToList();
                var ressponses = new List<ProductResponse>();
                foreach (var product in categoryProducts)
                {
                    var response = ProductMapper.ToResponse(product);
                    response.CategoryName = await _categoryHttpService.GetCategoryNameByIdAsync(product.CategoryId);
                    ressponses.Add(response);
                }

                return ressponses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<ProductResponse> CreateAsync(ProductRequest request)
        {
            try
            {
                var product = ProductMapper.ToEntity(request);
                product.Id = Guid.NewGuid();

                await _productRepository.AddAsync(product);

                return ProductMapper.ToResponse(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<ProductResponse> UpdateAsync(ProductRequest request)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
                throw new Exception($"Product với id {request.Id} không tồn tại");

            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;

            await _productRepository.UpdateAsync(product);
            return ProductMapper.ToResponse(product);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    throw new Exception($"Product với id {id} không tồn tại");

                await _productRepository.DeleteAsync(id);

                return $"Xóa product '{product.Name}' thành công";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }
    }
}
