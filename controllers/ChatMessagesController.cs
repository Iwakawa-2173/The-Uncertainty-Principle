using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using Npgsql; 
using System.Threading.Tasks;

namespace The_Uncertainty_Principle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase, IChatMessagesController // Реализация интерфейса
    {
        private readonly string _connectionString;

        public ChatMessagesController(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Получить все сообщения чата
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages()
        {
            var chatMessages = new List<ChatMessage>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM chat_messages", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        chatMessages.Add(new ChatMessage
                        {
                            MessageId = reader.GetInt32(0),
                            PlayerId = reader.GetInt32(1),
                            Message = reader.GetString(2),
                            CreatedAt = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return chatMessages;
        }

        // Получить сообщение чата по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(int id)
        {
            ChatMessage chatMessage = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM chat_messages WHERE message_id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            chatMessage = new ChatMessage
                            {
                                MessageId = reader.GetInt32(0),
                                PlayerId = reader.GetInt32(1),
                                Message = reader.GetString(2),
                                CreatedAt = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }

            if (chatMessage == null) return NotFound();
            return chatMessage;
        }

        // Создать сообщение чата
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> CreateChatMessage(ChatMessage chatMessage)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("INSERT INTO chat_messages (player_id, message) VALUES (@player_id, @message) RETURNING message_id", connection))
                {
                    command.Parameters.AddWithValue("player_id", chatMessage.PlayerId);
                    command.Parameters.AddWithValue("message", chatMessage.Message);

                    var newId = await command.ExecuteScalarAsync();
                    chatMessage.MessageId = Convert.ToInt32(newId);
                }
            }

            return CreatedAtAction(nameof(GetChatMessage), new { id = chatMessage.MessageId }, chatMessage);
        }

        // Обновить сообщение чата
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChatMessage(int id, ChatMessage chatMessage)
        {
            if (id != chatMessage.MessageId) return BadRequest();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("UPDATE chat_messages SET player_id = @player_id, message = @message WHERE message_id = @id", connection))
                {
                    command.Parameters.AddWithValue("player_id", chatMessage.PlayerId);
                    command.Parameters.AddWithValue("message", chatMessage.Message);
                    command.Parameters.AddWithValue("id", id);

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0) return NotFound();
                }
            }

            return NoContent();
        }

        // Удалить сообщение чата
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM chat_messages WHERE message_id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0) return NotFound();
                }
            }

            return NoContent();
        }
    }

    // Модель сообщения чата
    public class ChatMessage
    {
        public int MessageId { get; set; }
        public int PlayerId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
