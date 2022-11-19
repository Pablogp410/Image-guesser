using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using PictureGame.Core.Domain.User;
using PictureGame.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace PictureGame.Pages;

public class LogoutModel : PageModel
{
    private readonly IMediator _mediator;
    public LogoutModel(IMediator mediator) => _mediator = mediator;
    public string[] Errors { get; private set; } = System.Array.Empty<string>();

    [BindProperty]
    public User? user { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("./Index");
    }
}