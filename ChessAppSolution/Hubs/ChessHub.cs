using Microsoft.AspNetCore.SignalR; //Server automatically pushes updates to clients.

namespace ChessAppSolution.Hubs
{
    public class ChessHub : Hub
    {
        public async Task SendMove(string gameId, string fen, string moveFrom, string moveTo)
        {
            // Broadcast the move to other players in the game group
            await Clients.Group(gameId).SendAsync("ReceiveMove", fen, moveFrom, moveTo);
        }

        public async Task JoinGame(string gameId)
        {
            // Add client to game group for targeted updates
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }
    }
}