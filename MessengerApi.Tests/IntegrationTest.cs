using MessengerApi.Models;
using MessengerApi.Services;
using MessengerApi.Storage;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MessengerApi.Tests;

public class IntegrationTest
{
    [Fact]
    public async Task FullMessageFlow_ShouldStoreAndRetrieveMessage()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        using var context = new AppDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        var service = new MessageService(context);

        var userA = await service.CreateUserAsync("Alice");
        var userB = await service.CreateUserAsync("Bob");

        var conversationId = Guid.NewGuid();
        context.Conversations.Add(new Conversation { Id = conversationId, Type = "direct" });
        await context.SaveChangesAsync();

        await service.SendMessageAsync(conversationId, userA.Id, "Hello Bob! Are you offline?");

        var history = await service.GetConversationHistoryAsync(conversationId);

        Assert.Single(history);
        Assert.Equal("Hello Bob! Are you offline?", history[0].Text);
        Assert.Equal(userA.Id, history[0].SenderId);
    }
}