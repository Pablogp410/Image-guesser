using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PictureGame.Core.Domain.User;
using PictureGame.Core.Domain.Player;
using PictureGame.SharedKernel;

namespace PictureGame.Infrastructure.Data;

public class GameContext : DbContext
{
	private readonly IMediator _mediator;

	public GameContext(DbContextOptions configuration, IMediator mediator) : base(configuration)
	{
		_mediator = mediator;
	}

	public DbSet<User> Users { get; set; } = null!;
	public DbSet<Player> Players { get; set; } = null!;

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
		base.OnModelCreating(modelBuilder);
}

public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		// ignore events if no dispatcher provided
		if (_mediator == null) return result;

		// dispatch events only if save was successful
		var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
			.Select(e => e.Entity)
			.Where(e => e.Events.Any())
			.ToArray();

		foreach (var entity in entitiesWithEvents)
		{
			var events = entity.Events.ToArray();
			entity.Events.Clear();
			foreach (var domainEvent in events)
			{
				await _mediator.Publish(domainEvent, cancellationToken);
			}
		}
		return result;
	}

	public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
}

