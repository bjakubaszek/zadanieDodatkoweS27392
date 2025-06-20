using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanieDodatkowes27392.Models;

[Table("Participant")]
public class Participant
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(200)]
    public string LastName { get; set; } = null!;
    [MaxLength(200)]
    public string? Email { get; set; }

    public ICollection<EventParticipant> EventParticipants { get; set; } = null!;
}