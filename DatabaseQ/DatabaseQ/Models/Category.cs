using System.ComponentModel.DataAnnotations;

namespace DatabaseQ.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsDelete { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
