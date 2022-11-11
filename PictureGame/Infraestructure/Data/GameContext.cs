using Microsoft.EntityFrameworkCore;
using PictureGame.Domain.User;

namespace PictureGame.Infrastructure.Data;

// This class should inherit from the EntityFramework DbContext
public class GameContext : DbContext
{

    public GameContext (DbContextOptions<GameContext> options) : base(options) {
    }
    public DbSet<User> Users => Set<User>();

        
}