using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;

namespace PictureGame.Core.Domain.User.Pipelines;

public class GetById
{
	public record Request(Guid Id) : IRequest<User?>;

	public class Handler : IRequestHandler<Request, User?>
	{
		private readonly GameContext _db;

		public Handler(GameContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task<User?> Handle(Request request, CancellationToken cancellationToken)
		{
            var user = await _db.Users
                .Where(u => u.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
			return user;
		}
	}
}
