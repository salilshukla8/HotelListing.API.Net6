using HotelListing.API.Data;

namespace HotelListing.API.Core.Contracts
{
    public interface IHotelRespository : IGenericRepository<Hotel>
    {
        Task<Hotel> GetDetails(int id);
    }
}
