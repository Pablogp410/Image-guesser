using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PictureGame.Infrastructure.Data;
using PictureGame.SharedKernel;
using MediatR;

namespace PictureGame.Core.Domain.Game.Pipelines;

public class CreateGame
{
	public record Request(Guid Id) : IRequest<Response>;

	public record Response(bool Success, Game createdGame, string[] Errors);

	public class Handler : IRequestHandler<Request, Response>
	{
		private readonly GameContext _db;
		private readonly IEnumerable<IValidator<Game>> _validators;
		private readonly IGetRandomImageService _randimg;

		public Handler(GameContext db, IEnumerable<IValidator<Game>> validators, IGetRandomImageService randimg)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
			_validators = validators ?? throw new ArgumentNullException(nameof(validators));
			_randimg = randimg ?? throw new ArgumentNullException(nameof(randimg));

		}

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
                var game = new Game();

                game.Completed = false;

				game.playerID = request.Id;
                
                var errors = _validators.Select(v => v.IsValid(game))
                            .Where(result => !result.IsValid)
                            .Select(result => result.Error)
                            .ToArray();
                if (errors.Length > 0)
                {
                    return new Response(Success: false, game, errors);
                }
				var a_image = _randimg.GetRandomImage();
				if (a_image == null)
				{return new Response(Success: false, game, new string[] { "No images found" });}
				game.TheImage = a_image;
				game.AddPiece();
                _db.TheGame.Add(game);
                await _db.SaveChangesAsync(cancellationToken);

                return new Response(true, game, Array.Empty<string>());
		}
	}
}
