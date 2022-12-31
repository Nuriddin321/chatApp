using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace chatApi.Entities;

public class AppUser : IdentityUser
{
    public  bool IsAdmin { get; set; }
    public  Guid? ChatGroupId { get; set; }
    [ForeignKey("ChatGroupId")]
    public virtual ChatGroup? ChatGroup { get; set; }
}