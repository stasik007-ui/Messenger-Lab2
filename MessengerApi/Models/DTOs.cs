namespace MessengerApi.Models;

public record CreateUserRequest(string Name);
public record SendMessageRequest(Guid ConversationId, Guid SenderId, string Text);