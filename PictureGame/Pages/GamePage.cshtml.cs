using PictureGame.Domain.User;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PictureGame.Pages;

public class GamePageModel : PageModel
{
    private readonly ILogger<GamePageModel> _logger;
    private readonly IUserProvider _provider;

    public string[] error = new string[] { };

    private readonly IUserValidator _validator;
    public IEnumerable<User> Users {get; set;}

    public GamePageModel(IUserProvider provider, IUserValidator validator)
    {
         _provider = provider;
        _validator = validator;
    }

    [BindProperty]
    public User? user { get; set; }

     public async Task OnGetAsync()
        {
            Users = await _provider.GetUsers();
        }

    public async Task<IActionResult> OnPostAsync(User user)
        {
            if (user == null) {
            return Page();
        }
        if (_validator.IsValid(user).Length == 0) {
            foreach (var u in Users) {
                if (u.Username == user.Username && u.Password == user.Password && u.Name == user.Name) {
                    return RedirectToPage("Game", new {id = u.Id});
                } 
            }
            error = new string[] {"Invalid username or password"};
            return RedirectToPage("./index");
        } else {
            error = _validator.IsValid(user);
            return Page();
        }
    }
    
}
