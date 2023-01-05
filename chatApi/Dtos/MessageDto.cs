namespace chatApi.Dtos;

public class MessageDto
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public Guid? ChatGroupId { get; set; }
}