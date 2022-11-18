using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PictureGame.Infrastructure.Data;
using PictureGame.SharedKernel;
using MediatR;

namespace PictureGame.Core.Domain.Player.Pipelines;

public class CreatePlayer
{
    public record Request(int idUser, string username) : IRequest<Player>;

    public class Handler : IRequestHandler<Request, Player>
    {
        private readonly GameContext _db;

        public Handler(GameContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Player> Handle(Request request, CancellationToken cancellationToken)
        {
            var player = new Player(request.idUser,request.username);
            _db.Players.Add(player);
            await _db.SaveChangesAsync(cancellationToken);
            return player;
        }
    }
}
