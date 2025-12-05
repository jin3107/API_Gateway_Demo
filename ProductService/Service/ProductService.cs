using ProductService.DTO;
using ProductService.Models;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository<Product> _productRepository;

        public ProductService(IProductRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return products.Select(ProductMapper.ToResponse).ToList();
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

                return ProductMapper.ToResponse(product);
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

                return categoryProducts.Select(ProductMapper.ToResponse).ToList();
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
