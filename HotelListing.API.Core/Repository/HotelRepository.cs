using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRespository
    {
        private readonly HotelListingDbContext _context;

        public HotelRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
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
