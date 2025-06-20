using System.ComponentModel.DataAnnotations;

public class RegistrationResultDto
{
    public int EventId { get; set; }

    public int ParticipantId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = null!;
}