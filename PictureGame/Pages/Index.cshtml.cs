using PictureGame.Domain.User;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PictureGame.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUserProvider _provider;

    public string[] error = new string[] { };

    private readonly IUserValidator _validator;

    public IndexModel(IUserProvider provider, IUserValidator validator)
    {
         _provider = provider;
        _validator = validator;
    }

    public User? user { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        
        return Page();
    }
    
}
