namespace PictureGame.Domain.Game
{

class Game : BaseEntity
{
    public Game(){}

    public Player player { get; set; }
    public ScoreBoard scoreboard { get; set; }
    
    public int points()
    {
        return the_points;
    }
}
}