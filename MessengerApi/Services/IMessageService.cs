using MessengerApi.Models;

namespace MessengerApi.Services;

public interface IMessageService
{
    Task<User> CreateUserAsync(string name);
    Task<Message> SendMessageAsync(Guid conversationId, Guid senderId, string text);
    Task<List<Message>> GetConversationHistoryAsync(Guid conversationId);
}