using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

public interface ISeatService
{
    Task<GetSeatDto> CreatePlaceAsync(SaveSeatDto dto);
    Task<PagedResult<GetSeatDto>> GetAllPlacesAsync(int page, int size);
    Task<GetSeatDto> GetPlaceAsync(long id);
    Task<GetSeatDto> UpdatePlaceAsync(long id, SaveSeatDto dto);
    Task DeletePlaceAsync(long id);
}
