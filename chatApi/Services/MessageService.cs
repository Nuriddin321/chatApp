using chatApi.Data;
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

    public async Task CreateChatGroup(ChatGroup group)
    {
        _context.ChatGroups?.Add(group);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChatGroup>> GetGroups()
    {
        var groups = await _context.ChatGroups.ToListAsync();
        return groups;
    }

    public async Task<Tuple<bool, string?>> IsContainsKey(string key)
    {
        var group = await  _context.ChatGroups.FirstOrDefaultAsync(g => g.Key == key);
        bool isContainsKey = false;
        if(group is null)
            return new Tuple<bool, string?> ( isContainsKey, group?.Name);

        isContainsKey = true;

        return new Tuple<bool, string?> ( isContainsKey, group?.Name);;
    }
}