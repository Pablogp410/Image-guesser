using System;
using PictureGame.SharedKernel;

namespace PictureGame.Core.Domain.Player;

public class Player : BaseEntity 
{
    public Player() {
        Username = "Guest";
        Type = "Guesser";
        Score = 0;
    }

    public Player(Guid id) {
        Id = id;
    }

    public Player(Guid idUser, string username) {
        Id = idUser;
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Type = "Guesser";
        Score = 0;
    }

    public Guid Id { get; protected set; }
    public string Username { get; set; }
    public string Type { get; set; }
    public int Score { get; set; }

    
}
