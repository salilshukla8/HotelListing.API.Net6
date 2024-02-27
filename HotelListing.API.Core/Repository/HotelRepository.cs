using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRespository
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public HotelRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            _mapper = mapper;
        }

        public async Task<HotelDto> GetDetails(int id)
        {
            var hotel = await _context.Hotels
                .ProjectTo<HotelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (hotel is null)
                throw new NotFoundException(nameof(GetDetails), id);
            return hotel;
        }
    }

}
