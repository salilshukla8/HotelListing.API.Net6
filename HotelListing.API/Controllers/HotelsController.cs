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

            return Ok(hotel);
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

            try
            {
                await _hotelRepository.UpdateAsync(id,updateHotelDto);
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

            var hotel = await _hotelRepository.AddAsync<CreateHotelDto, GetHotelDto>(createHotelDto);

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotelRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
