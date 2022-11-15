using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PictureGame.Infrastructure.Data;
using PictureGame.SharedKernel;
using MediatR;

namespace PictureGame.Core.Domain.User.Pipelines;

public class Create
{
	public record Request(string Name, string username, string password) : IRequest<Response>;

	public record Response(bool Success, User createdUser, string[] Errors);

	public class Handler : IRequestHandler<Request, Response>
	{
		private readonly GameContext _db;
		private readonly IEnumerable<IValidator<User>> _validators;

		public Handler(GameContext db, IEnumerable<IValidator<User>> validators)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
			_validators = validators ?? throw new ArgumentNullException(nameof(validators));
		}

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			var user = new User(request.username, request.Name, request.password)
			{
				Username = request.username,
				Name = request.Name,
                Password = request.password,
                Score = 0
			};

			var errors = _validators.Select(v => v.IsValid(user))
						.Where(result => !result.IsValid)
						.Select(result => result.Error)
						.ToArray();
			if (errors.Length > 0)
			{
				return new Response(Success: false, user, errors);
			}

			_db.Users.Add(user);
			await _db.SaveChangesAsync(cancellationToken);

			return new Response(true, user, Array.Empty<string>());
		}
	}
}
