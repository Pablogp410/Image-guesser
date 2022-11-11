using PictureGame.Domain.User;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PictureGame.Pages;

public class RegisterModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUserProvider _provider;

    public string[] error = new string[] { };

    private readonly IUserValidator _validator;

    public RegisterModel(IUserProvider provider, IUserValidator validator)
    {
         _provider = provider;
        _validator = validator;
    }

    [BindProperty]
    public User? user { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (user == null)
        {
            return Page();
        }
         if (_validator.IsValid(user).Length == 0) {
            await _provider.AddUser(user);
            return RedirectToPage("./index");
        } else {
            error = _validator.IsValid(user);
            return Page();
        }
    }
    
}
