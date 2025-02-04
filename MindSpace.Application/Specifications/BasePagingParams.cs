namespace MindSpace.Application.Specifications
{
    public class BasePagingParams
    {
        // Page Index
        // - Start a 1 
        public int PageIndex { get; set; } = 1;

        // Page Size
        private const int MaxPageSize = 20;
        private int _pageSize = 5;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
