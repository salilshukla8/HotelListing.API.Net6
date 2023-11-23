using HotelListing.API.Data;

namespace HotelListing.API.Contracts
{
    public interface IHotelRespository : IGenericRepository<Hotel>
    {
        Task<Hotel> GetDetails(int id);
    }
}
