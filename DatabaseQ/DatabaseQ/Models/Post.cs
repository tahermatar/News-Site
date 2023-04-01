using System.ComponentModel.DataAnnotations;

namespace DatabaseQ.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Body { get; set; }
        public string? ImageUrl { get; set; }
        public int TimeToRead { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}
