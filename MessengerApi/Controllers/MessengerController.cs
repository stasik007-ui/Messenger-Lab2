using MessengerApi.Models;
using MessengerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessengerApi.Controllers;

[ApiController]
public class MessengerController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessengerController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var user = await _messageService.CreateUserAsync(request.Name);
            return Ok(user);
        }
        catch (ArgumentException ex) { return BadRequest(new { Error = ex.Message }); }
    }

    [HttpPost("messages")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        try
        {
            var message = await _messageService.SendMessageAsync(request.ConversationId, request.SenderId, request.Text);
            return Ok(message);
        }
        catch (ArgumentException ex) { return BadRequest(new { Error = ex.Message }); }
        catch (KeyNotFoundException ex) { return NotFound(new { Error = ex.Message }); }
    }

    [HttpGet("conversations/{id}/messages")]
    public async Task<IActionResult> GetMessages(Guid id)
    {
        try
        {
            var messages = await _messageService.GetConversationHistoryAsync(id);
            return Ok(messages);
        }
        catch (KeyNotFoundException ex) { return NotFound(new { Error = ex.Message }); }
    }
}