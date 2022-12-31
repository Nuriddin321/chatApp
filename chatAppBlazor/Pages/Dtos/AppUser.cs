using System.ComponentModel.DataAnnotations.Schema;

namespace chatAppBlazor.Pages.Dtos;
public class AppUser  
{
    public string? Id { get; set; }
    public string? Username { get; set; }
    public bool IsAdmin { get; set; }
    public Guid? ChatGroupId { get; set; }
    [ForeignKey("ChatGroupId")]
    public virtual ChatGroup? ChatGroup { get; set; }
}
