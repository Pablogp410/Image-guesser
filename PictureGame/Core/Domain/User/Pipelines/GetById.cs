using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;

namespace PictureGame.Core.Domain.User.Pipelines;

public class GetByUser
{
	public record Request(string Username, string password) : IRequest<User?>;

	public class Handler : IRequestHandler<Request, User?>
	{
		private readonly GameContext _db;

		public Handler(GameContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task<User?> Handle(Request request, CancellationToken cancellationToken)
		{
            var user = await _db.Users
                .Where(u => u.Username == request.Username && u.Password == request.password)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
			return user;
		}
	}
}
