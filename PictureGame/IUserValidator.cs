namespace PictureGame;
using PictureGame.Domain.User;

public interface IUserValidator
{
    string[] IsValid(User user);
}

public class UserValidator : IUserValidator
{
    public string[] IsValid(User user)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            errors.Add("Name is required");
        }
        if (string.IsNullOrWhiteSpace(user.Username))
        {
            errors.Add("UserName is required");
        }
        if (string.IsNullOrWhiteSpace(user.Password))
        {
            errors.Add("Password is required");
        }
        
        return errors.ToArray();
    }
}