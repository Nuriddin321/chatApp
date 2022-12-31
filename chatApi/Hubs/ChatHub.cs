using System.Security.Claims;
using chatApi.Entities;
using chatApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace chatApi.Hubs;

public class ChatHub : Hub
{
    private readonly MessageService _messageService;
    private readonly UserManager<AppUser> _userManager;

    public ChatHub(MessageService messageService, UserManager<AppUser> userManager)
    {
        _messageService = messageService;
        _userManager = userManager;
    }

    [Authorize]
    public async Task MessageYubor(string message)
    {
        var user = Context.User.FindFirstValue(ClaimTypes.Name);
        await Clients.All.SendAsync("MessageHandler", user, message);
    }

    [Authorize]
    public async Task SendMessageToGroup(string group, string message)
    {
        var username = Context.User.FindFirstValue(ClaimTypes.Name);

        await Clients.All.SendAsync("MessageFromApiToBlazorForGroups", username, message);
    }

    [Authorize]
    public async Task CreateChatGroup(string name)
    {
        // var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.GetUserAsync(Context.User);

        var createGroup = new ChatGroup()
        {
            Name = name,
            UserId = user.Id,
            Key = Guid.NewGuid().ToString("N")[..10],
        };

        await _messageService.CreateChatGroup(createGroup);

        user.IsAdmin = true;
        await _userManager.UpdateAsync(user);

        var group = await _messageService.GetGroups();

        await Clients.All.SendAsync("GroupCreated", group, user.UserName);
    }

    public async Task GroupgaQoshilish(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }
}