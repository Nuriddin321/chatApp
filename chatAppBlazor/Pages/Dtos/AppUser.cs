namespace chatAppBlazor.Pages.Dtos;
public class AppUser
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public bool IsAdmin { get; set; }
    public Guid? ChatGroupId { get; set; } 
}
