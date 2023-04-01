using DatabaseQ.Models;
using System.ComponentModel.DataAnnotations;

namespace DatabaseQ.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }
        public string? ImageUrl { get; set; }
        public int TimeToRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; }
        //public List<PostTag> PostTags { get; set; }
    }
}
