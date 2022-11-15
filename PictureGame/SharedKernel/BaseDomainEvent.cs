using System;
using MediatR;

namespace PictureGame.SharedKernel;

public abstract record BaseDomainEvent : INotification
{
	public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}