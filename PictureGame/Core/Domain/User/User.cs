using System;
using PictureGame.SharedKernel;

namespace PictureGame.Core.Domain.User;

public class User : BaseEntity 
{
    public User() {
    }

    public User(string username, string name, string password, Guid id) {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Score = 0;
        Password = password ?? throw new ArgumentNullException(nameof(password));
        Id = id;
    }
    public Guid Id { get; protected set; }
    public string Username { get; set; } 
    public string Name { get; set; }
    public int Score { get; set; }
    public string Password { get; set; }

    
}
