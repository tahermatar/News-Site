namespace DatabaseQ.ViewModel
{
    public class PagingResultViewModel
    {
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public object Data { get; set; }
    }
}
