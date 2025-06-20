using System.ComponentModel.DataAnnotations;

public class ParticipantShortDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string LastName { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
    [MaxLength(200)]
    public string? Email { get; set; }
}