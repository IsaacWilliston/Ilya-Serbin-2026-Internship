namespace SeatsReservationDotNet.DTOs;

public class GetHallDto
{
    /// <summary>Unique hall identifier.</summary>
    public long Id {get; set;}
    
    /// <summary>Parent cinema identifier.</summary>
    public long CinemaId {get; set;}
    
    /// <summary>Hall name (e.g. "Hall A").</summary>
    public string? Name {get; set;}
}