using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Models;
using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        //private readonly HotelListingDbContext _context;
        private readonly IHotelRespository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRespository hotelRespository, IMapper mapper)
        {
            _hotelRepository = hotelRespository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync<GetHotelDto>();
            return Ok(hotels);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GetHotelDto>>> GetHotels([FromQuery] QueryParameters queryParameters)
        {
            var pagedHotels = await _hotelRepository.GetAllSync<GetHotelDto>(queryParameters);
            return Ok(pagedHotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
        {

            var hotel = await _hotelRepository.GetDetails(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDto = _mapper.Map<GetHotelDto>(hotel);

            return Ok(hotelDto);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var hotel = await _hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(updateHotelDto, hotel);

            try
            {
                await _hotelRepository.UpdateAsync(hotel);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);

            await _hotelRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
