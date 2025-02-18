using System.Collections.Generic;

namespace PictureGame.SharedKernel;

public interface IValidator<T>
{
	(bool IsValid, string Error) IsValid(T item);
}