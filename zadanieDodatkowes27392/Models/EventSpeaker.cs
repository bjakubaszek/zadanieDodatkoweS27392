using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace zadanieDodatkowes27392.Models;

[Table("EventSpeaker")]
[PrimaryKey(nameof(EventId),nameof(SpeakerId))]

public class EventSpeaker
{
        
    [Column("Event_ID")]
    public int EventId { get; set; }
    [Column("Speaker_ID")]
    public int SpeakerId { get; set; }

    [ForeignKey(nameof(EventId))]
    public virtual Event Event { get; set; } = null!;
    
    [ForeignKey(nameof(SpeakerId))]
    public virtual Speaker Speaker { get; set; } = null!;
}