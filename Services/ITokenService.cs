using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

public interface ITokenService
{
    AuthResponseDto GenerateToken(UserEntity user);
}