namespace HotelListing.API.Core.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string name, object key) : base($"In {name}, {key} is an invalid record")
        {

        }
    }
}
