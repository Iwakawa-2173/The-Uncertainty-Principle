using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace The_Uncertainty_Principle.Controllers
{
    public interface IChatMessagesController
    {
        Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages();
        Task<ActionResult<ChatMessage>> GetChatMessage(int id);
        Task<ActionResult<ChatMessage>> CreateChatMessage(ChatMessage chatMessage);
        Task<IActionResult> UpdateChatMessage(int id, ChatMessage chatMessage);
        Task<IActionResult> DeleteChatMessage(int id);
    }

    public interface IEventsController
    {
        ActionResult<IEnumerable<Event>> GetEvents();
        ActionResult<Event> GetEvent(int id);
        ActionResult<Event> CreateEvent(Event eventItem);
        IActionResult UpdateEvent(int id, Event eventItem);
        IActionResult DeleteEvent(int id);
    }

    public interface IPlayersController
    {
        ActionResult<IEnumerable<Player>> GetPlayers();
        ActionResult<Player> GetPlayer(int id);
        ActionResult<Player> CreatePlayer(Player player);
        IActionResult UpdatePlayer(int id, Player player);
        IActionResult DeletePlayer(int id);
    }

    public interface IResponsesController
    {
        ActionResult<IEnumerable<Response>> GetResponses();
        ActionResult<Response> GetResponse(int id);
        ActionResult<Response> CreateResponse(Response response);
        IActionResult UpdateResponse(int id, Response response);
        IActionResult DeleteResponse(int id);
    }

    public interface IScoreScalesController
    {
        ActionResult<IEnumerable<ScoreScale>> GetScoreScales();
        ActionResult<ScoreScale> GetScoreScale(int id);
        ActionResult<ScoreScale> CreateScoreScale(ScoreScale scoreScale);
        IActionResult UpdateScoreScale(int id, ScoreScale scoreScale);
        IActionResult DeleteScoreScale(int id);
    }
}