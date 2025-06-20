using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanieDodatkowes27392.Models;

[Table("Speaker")]
public class Speaker
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(200)]
    public string LastName { get; set; } = null!;
    [MaxLength(400)]
    public string? Bio { get; set; }
    
    public ICollection<EventSpeaker> EventSpeakers { get; set; } = null!;

}