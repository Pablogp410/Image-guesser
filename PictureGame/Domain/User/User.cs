namespace PictureGame.Domain.User;

public class User 
{
    public int Id { get; set; }
    public string Username { get; set; } 
    public string Name { get; set; }
    public int Score { get; set; } = 0;
    public string Password { get; set; }
    
}
