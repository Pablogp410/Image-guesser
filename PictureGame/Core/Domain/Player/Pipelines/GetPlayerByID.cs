using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;


namespace PictureGame.Core.Domain.Player.Pipelines;

public class GetPlayerById
{
	public record Request (Guid Id) : IRequest<Player>{ }

	public class Handler : IRequestHandler<Request, Player>
	{
		private readonly GameContext _db;

		public Handler(GameContext db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public async Task<Player> Handle(Request request, CancellationToken cancellationToken)
		{
            var player = await _db.Players
                .Where(u => u.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
			return player;
		}
	}
}
