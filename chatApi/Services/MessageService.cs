using chatApi.Data;
using chatApi.Dtos;
using chatApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace chatApi.Services;

public class MessageService
{
    private readonly AppDbContext _context;

    public MessageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateGroup(ChatGroup group)
    {
        _context.ChatGroups?.Add(group);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChatGroup>> GetGroups()
    {
        var groups = await _context.ChatGroups.ToListAsync();
        return groups;
    }

    public async Task<List<MessageDto>?> GetGroupMessages(string groupName)
    {
        var group = await _context.ChatGroups.FirstOrDefaultAsync(g => g.Name == groupName);
        
        List<MessageDto> listMessageDto = new ();

        foreach(var message in group.Messages)
        {
            var messageDto = new MessageDto()
            {
                Id = message.Id,
                Text = message.Text,
                ChatGroupId = message.ChatGroupId
            };

            listMessageDto.Add(messageDto);
        }

        return listMessageDto;
    }

    public async Task AddMessageToGroup(string groupName, string text)
    {
        var group = await _context.ChatGroups.FirstOrDefaultAsync(g => g.Name == groupName);

        var message = new Message()
        {
            Text = text,
            ChatGroupId = group.Id
        };

        group.Messages.Add(message);

        _context.ChatGroups.Update(group);

        await _context.SaveChangesAsync();
    }

    public async Task<Tuple<bool, string?>> IsContainsKey(string key)
    {
        var group = await _context.ChatGroups.FirstOrDefaultAsync(g => g.Key == key);
        bool isContainsKey = false;
        if (group is null)
            return new Tuple<bool, string?>(isContainsKey, group?.Name);

        isContainsKey = true;

        return new Tuple<bool, string?>(isContainsKey, group?.Name); ;
    }

}