using System.ComponentModel.DataAnnotations;

namespace DatabaseQ.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PostCount { get; set; }
        public DateTime? LastPostAdded { get; set; }
    }
}
