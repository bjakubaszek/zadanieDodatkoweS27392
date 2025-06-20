using System.ComponentModel.DataAnnotations;

public class SpeakerShortDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string LastName { get; set; } = null!;
}