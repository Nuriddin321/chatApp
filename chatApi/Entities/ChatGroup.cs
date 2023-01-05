using System.ComponentModel.DataAnnotations.Schema;

namespace chatApi.Entities;

public class ChatGroup
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Key { get; set; }
    public Guid? MessageId {get; set;}

    // [ForeignKey("MessagesId")]
    public virtual List<Message>? Messages {get; set;}

    public string? UserId { get; set; }

    // [ForeignKey("UserId")]
    public virtual AppUser? User { get; set; }
}