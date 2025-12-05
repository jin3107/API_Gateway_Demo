using System.ComponentModel.DataAnnotations;

namespace CategoryService.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
