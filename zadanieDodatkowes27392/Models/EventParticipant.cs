using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace zadanieDodatkowes27392.Models;


[Table("EventParticipant")]
[PrimaryKey(nameof(EventId),nameof(ParticipantId))]
public class EventParticipant
{
    
    [Column("Event_ID")]
    public int EventId { get; set; }
    [Column("Participant_ID")]
    public int ParticipantId { get; set; }

    [ForeignKey(nameof(EventId))]
    public virtual Event Event { get; set; } = null!;
    
    [ForeignKey(nameof(ParticipantId))]
    public virtual Participant Participant { get; set; } = null!;
}