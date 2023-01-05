using System.ComponentModel.DataAnnotations.Schema;

namespace chatApi.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public Guid? ChatGroupId { get; set; }
    
    [ForeignKey("ChatGroupId")]
    public virtual ChatGroup? ChatGroup { get; set; }
}