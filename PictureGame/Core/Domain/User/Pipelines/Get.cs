using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;

namespace PictureGame.Core.Domain.User.Pipelines;

public class Get
{
	public record Request : IRequest<List<User>> { }

	public class Handler : IRequestHandler<Request, List<User>>
	{
		private readonly GameContext _db;

		public Handler(GameContext db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public async Task<List<User>> Handle(Request request, CancellationToken cancellationToken)
			=> await _db.Users.OrderBy(i => i.Name).ToListAsync(cancellationToken: cancellationToken);
	}
}
