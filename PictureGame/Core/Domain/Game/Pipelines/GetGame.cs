using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;

namespace PictureGame.Core.Domain.Game.Pipelines;

public class GetGame
{
	public record Request(Guid Id) : IRequest<Game> { }

	public class Handler : IRequestHandler<Request, Game?>
	{
		private readonly GameContext _db;

		public Handler(GameContext db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public async Task<Game?> Handle(Request request, CancellationToken cancellationToken)
			=> await _db.TheGame.Where(u => u.playerID == request.Id).Include(u => u.CurrentImages).Include(u => u.TheImage).Include(u => u.TheImage.Pieces)
			.SingleOrDefaultAsync(cancellationToken: cancellationToken);
	}
}
