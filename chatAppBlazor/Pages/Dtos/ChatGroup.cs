using System.ComponentModel.DataAnnotations.Schema;

namespace chatAppBlazor.Pages.Dtos;

public class ChatGroup
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Key { get; set; }
    public string? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual AppUser? User { get; set; }
}
