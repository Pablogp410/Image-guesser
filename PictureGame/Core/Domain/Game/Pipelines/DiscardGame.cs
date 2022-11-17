using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PictureGame.Infrastructure.Data;
using PictureGame.SharedKernel;
using MediatR;

namespace PictureGame.Core.Domain.Game.Pipelines;

public class DiscardGame
{
	public record Request() : IRequest<Response>;

	public record Response(bool Success, Game DiscardedGame, string[] Errors);

   


	public class Handler : IRequestHandler<Request, Response>
	{
		private readonly GameContext _db;
		private readonly IEnumerable<IValidator<Game>> _validators;

        private readonly IMediator _mediator;


		public Handler(GameContext db, IEnumerable<IValidator<Game>> validators, IMediator mediator)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
			_validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _mediator = mediator;
		}

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			var game = await _mediator.Send(new Core.Domain.Game.Pipelines.GetGame.Request());

            var errors = _validators.Select(v => v.IsValid(game))
						.Where(result => !result.IsValid)
						.Select(result => result.Error)
						.ToArray();
			
            if (errors.Length > 0)
			{
				return new Response(Success: false, game, errors);
			}
			
            _db.TheGame.Remove(game);
			await _db.SaveChangesAsync(cancellationToken);

			return new Response(true, game, Array.Empty<string>());
		}
	}
}
