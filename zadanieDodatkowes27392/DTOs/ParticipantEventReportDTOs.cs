using System.ComponentModel.DataAnnotations;

public class ParticipantEventReportDto
{
    public int EventId { get; set; }

    [Required]
    [MaxLength(100)]
    public string EventTitle { get; set; } = null!;

    public DateTime Date { get; set; }

    [Required]
    public List<SpeakerShortDto> Speakers { get; set; } = new();
}