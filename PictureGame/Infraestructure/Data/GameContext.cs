using Microsoft.EntityFrameworkCore;
using PictureGame.Domain.User;
using PictureGame.Domain.Game;

namespace PictureGame.Infrastructure.Data;

// This class should inherit from the EntityFramework DbContext
public class GameContext : DbContext
{

    public GameContext (DbContextOptions<GameContext> options) : base(options) {
    }
    public DbSet<User> Users => Set<User>();

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Picture> Pictures => Set<Picture>();
    
}

internal class Images
{
    public static ImageItems[] ImageItems {get; private set;} 
    public List<Image> Pieces { get; set; }
}