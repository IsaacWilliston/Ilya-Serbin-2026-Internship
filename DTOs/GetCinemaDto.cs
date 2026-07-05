namespace SeatsReservationDotNet.DTOs;

public class GetCinemaDto
{
    /// <summary>Unique cinema identifier.</summary>
    public long Id { get; set; }
    
    /// <summary>Cinema name.</summary>
    public string? Name { get; set; }
    
    /// <summary>Street address.</summary>
    public string? Address { get; set; }
    
    /// <summary>City where the cinema is located.</summary>
    public string? City { get; set; }
}