using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

/// <summary>System user account.</summary>
[Table("users")]
public class UserEntity
{
    /// <summary>Unique user identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Unique email address.</summary>
    [Required]
    [Column("email")]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    /// <summary>Secure password hash.</summary>
    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    /// <summary>User role governing authorizations.</summary>
    [Column("role")]
    [MaxLength(50)]
    public Role Role { get; set; }
}