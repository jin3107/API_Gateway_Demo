using System.ComponentModel.DataAnnotations;

namespace CategoryService.DTO
{
    public class CategoryRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name must not exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Category description must not exceed 100 characters")]
        public string Description { get; set; }
    }
}
