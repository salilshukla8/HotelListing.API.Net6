using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Core.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

        Task<AuthResponseDto> Login(LoginDto loginDto);

        Task<string> CreateRefeshToken();

        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
