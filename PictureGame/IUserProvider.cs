using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PictureGame.Domain.User;
using PictureGame.Infrastructure.Data;

namespace PictureGame;

public interface IUserProvider
{
    Task<User[]> GetUsers();
    Task AddUser(User user);
    Task<User> GetUser(int id);
    Task UpdateUser(int id, User user);
}

public class UserProvider : IUserProvider {
    private readonly GameContext _context;
    private readonly IUserValidator _validator;

    public UserProvider(GameContext context, IUserValidator validator) {
        _context = context;
        _validator = validator;
    }

    public async Task<User[]> GetUsers() => await _context.Users.ToArrayAsync();

    // Add a new food item to the database
    public async Task AddUser(User user) {
        var errors = _validator.IsValid(user);
        if (errors.Length > 0) {
            throw new ArgumentException("Invalid user", nameof(user));
        }
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    // Get a food item from the database
    public async Task<User> GetUser(int id) {
        return await _context.Users.FindAsync(id);
    }

    // Remove a food item from the database
    public async Task UpdateUser(int id, User user) {
        var errors = _validator.IsValid(user);
        if (errors.Length > 0) {
            throw new ArgumentException("Invalid user", nameof(user));
        }
        var oldUser = await _context.Users.FindAsync(id);
        if (oldUser != null) {
            oldUser.Name = user.Name;
            oldUser.Username = user.Username;
            oldUser.Password = user.Password;
            await _context.SaveChangesAsync();
        }
    }
}