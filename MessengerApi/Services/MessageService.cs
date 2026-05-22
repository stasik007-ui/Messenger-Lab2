using MessengerApi.Models;
using MessengerApi.Storage;
using Microsoft.EntityFrameworkCore;

namespace MessengerApi.Services;

public class MessageService : IMessageService
{
    private readonly AppDbContext _dbContext;

    public MessageService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUserAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name cannot be empty.");

        var user = new User { Name = name };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<Message> SendMessageAsync(Guid conversationId, Guid senderId, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Message text cannot be empty.");

        var senderExists = await _dbContext.Users.AnyAsync(u => u.Id == senderId);
        if (!senderExists)
            throw new KeyNotFoundException("Sender does not exist.");

        var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(c => c.Id == conversationId);
        if (conversation == null)
        {
            // Автоматично створюємо розмову
            conversation = new Conversation { Id = conversationId, Type = "direct" };
            _dbContext.Conversations.Add(conversation);
            await _dbContext.SaveChangesAsync(); // Зберігаємо нову розмову в базу
        }

        var message = new Message
        {
            ConversationId = conversationId,
            SenderId = senderId,
            Text = text
        };

        _dbContext.Messages.Add(message);
        await _dbContext.SaveChangesAsync(); // Зберігаємо саме повідомлення

        return message;
    }


    public async Task<List<Message>> GetConversationHistoryAsync(Guid conversationId)
    {
        var conversationExists = await _dbContext.Conversations.AnyAsync(c => c.Id == conversationId);
        if (!conversationExists)
            throw new KeyNotFoundException("Conversation does not exist.");

        return await _dbContext.Messages
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }
}