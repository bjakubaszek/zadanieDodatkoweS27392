using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanieDodatkowes27392.Models;



[Table("Event")]
public class Event
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    [MaxLength(200)]
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    [Required]
    public int MaxParticipants { get; set; }

    public ICollection<EventSpeaker> EventSpeakers { get; set; } = null!;
    public ICollection<EventParticipant> EventParticipants { get; set; } = null!;
}