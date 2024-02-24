namespace HotelListing.API.Core.Models
{
    public class QueryParameters
    {
        const int maxPageSize = 50;

        private int _pageSize = 15;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get { return _pageSize; }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }


}
