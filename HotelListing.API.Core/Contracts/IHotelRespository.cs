using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Core.Contracts
{
    public interface IHotelRespository : IGenericRepository<Hotel>
    {
        Task<HotelDto> GetDetails(int id);
    }
}
