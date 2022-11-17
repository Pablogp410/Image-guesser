using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;


namespace PictureGame.Core.Domain.Player.Pipelines;

public class GetPlayers
{
	public record Request : IRequest<List<Player>> { }

	public class Handler : IRequestHandler<Request, List<Player>>
	{
		private readonly GameContext _db;

		public Handler(GameContext db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public async Task<List<Player>> Handle(Request request, CancellationToken cancellationToken)
			=> await _db.Players.OrderBy(i => i.Score)
			.Where(i => i.Score > 0)
			.ToListAsync(cancellationToken: cancellationToken);
	}
}
