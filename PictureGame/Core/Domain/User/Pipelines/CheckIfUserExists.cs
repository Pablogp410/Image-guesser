using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;

namespace PictureGame.Core.Domain.User.Pipelines;

public class CheckIfUserExists
{
	public record Request(string Username) : IRequest<Response>;

    public record Response(bool Success, User user, string[] Errors);


	public class Handler : IRequestHandler<Request, Response>
	{
		private readonly GameContext _db;

		public Handler(GameContext db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == request.Username, cancellationToken: cancellationToken);
			if (user == null)
			{
				return new Response(Success: true, user, Array.Empty<string>());
			}
			return new Response(Success: false, user,new string[] { "Username is taken" });
        }
	}
}