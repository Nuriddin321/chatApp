using System.Security.Claims;
using chatApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace chatApi.Hubs;

public class ChatHub : Hub
{
    private readonly MessageService _messageService;

    public ChatHub(MessageService messageService)
    {
        _messageService = messageService;
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
        
        _messageService.Messages[group].Add(new Tuple<string, string>(username, message));

        await Clients.All.SendAsync("MessageFromApiToBlazorForGroups", username, message);
    }

    public async Task GroupgaQoshilish(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

}