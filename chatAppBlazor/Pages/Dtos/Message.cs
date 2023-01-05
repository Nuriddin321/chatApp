namespace chatAppBlazor.Pages.Dtos;

public class Message
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public Guid? ChatGroupId { get; set; }
}