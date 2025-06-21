using System.ComponentModel.DataAnnotations;

public class EventCreateDto
{
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = null!;
    [Required]
    public DateTime Date { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int MaxParticipants { get; set; }
}

public class EventGetDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; }
    [Required]
    public List<SpeakerShortDto> Speakers { get; set; } = new();
    public int ParticipantCount { get; set; }
    public int FreePlaces => MaxParticipants - ParticipantCount;
}
