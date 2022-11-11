using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PictureGame.Domain.User;

namespace PictureGame.Pages;

public class GameModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUserProvider _provider;

    public string[] error = new string[] { };
 

    private readonly IUserValidator _validator;
    public IEnumerable<User> Users {get; set;}

    public GameModel(IUserProvider provider, IUserValidator validator)
    {
        _provider = provider;
        _validator = validator;
    }
    
    [BindProperty]
    public User? user {get; set;}

   

     public async Task OnGetAsync(int id)
        {
            Users = await _provider.GetUsers();
            user = await _provider.GetUser(id);
        }
}