using System.ComponentModel.DataAnnotations.Schema;

namespace DreamChip.AnimalTracking.Domain.Entities;

/// <summary>
/// Account entity.
/// </summary>
public sealed class Account : BaseEntity<int>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="firstName">First Name.</param>
    /// <param name="lastName">Last Name.</param>
    /// <param name="email">Email.</param>
    /// <param name="password">Password.</param>
    public Account(string firstName,
        string lastName, 
        string email,
        string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;

        Animals = new List<Animal>();
    }

    /// <summary>
    /// User first name.
    /// </summary>
    [Column("first_name")]
    public required string FirstName { get; set; }

    /// <summary>
    /// User last name.
    /// </summary>
    [Column("last_name")]
    public required string LastName { get; set; }

    /// <summary>
    /// User email.
    /// </summary>
    [Column("email")]
    public required string Email { get; set; }

    /// <summary>
    /// Hash of user's password.
    /// </summary>
    [Column("password")]
    public required string Password { get; set; }

    /// <summary>
    /// The animals, chipped by the user.
    /// </summary>
    public List<Animal> Animals { get; set; }
}
