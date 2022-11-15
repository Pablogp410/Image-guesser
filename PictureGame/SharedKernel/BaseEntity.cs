using System.Collections.Generic;

namespace PictureGame.SharedKernel;

public abstract class BaseEntity
{
	public List<BaseDomainEvent> Events = new();
}