using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PictureGame.Infrastructure.Data;
using PictureGame.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PictureGame.Core.Domain.Game.Pipelines;

public class AddPiece
{
	public record Request(int Id) : IRequest<Response>;

	public record Response(bool Success, Game createdGame, string[] Errors);

	public class Handler : IRequestHandler<Request, Response>
	{
		private readonly GameContext _db;
		private readonly IEnumerable<IValidator<Game>> _validators;
	

		public Handler(GameContext db, IEnumerable<IValidator<Game>> validators)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
			_validators = validators ?? throw new ArgumentNullException(nameof(validators));
			

		}

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
                var game = await _db.TheGame.Where(u => u.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);

                var errors = _validators.Select(v => v.IsValid(game))
                            .Where(result => !result.IsValid)
                            .Select(result => result.Error)
                            .ToArray();
                if (errors.Length > 0)
                {
                    return new Response(Success: false, game, errors);
                }
				game.AddPiece();
                _db.TheGame.Update(game);
                await _db.SaveChangesAsync(cancellationToken);
                return new Response(true, game, Array.Empty<string>());
		}
	}
}
