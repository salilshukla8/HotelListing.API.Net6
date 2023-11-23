using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRespository
    {
        private readonly HotelListingDbContext _context;

        public HotelRepository(HotelListingDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Hotel> GetDetails(int id)
        {
            var data = await _context.Hotels.FirstOrDefaultAsync(q => q.Id == id);
            return data;
        }
    }
}
