using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CountriesV2Controller : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly IMapper _mapper;

        public CountriesV2Controller(ICountriesRepository countriesRepository, IMapper mapper)
        {
            //_context = context;
            _mapper = mapper;
            _countriesRepository = countriesRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            var country = _mapper.Map<Country>(createCountryDto);

            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            //var countries = await _context.Countries.ToListAsync();
            var countries = await _countriesRepository.GetAllAsync<GetCountryDto>();
            //var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                throw new BadRequestException(nameof(PutCountry), id);
            }

            //var country = await _context.Countries.FindAsync(id);

            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(PutCountry), id);
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);

            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(DeleteCountry), id);
            }

            await _countriesRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}

