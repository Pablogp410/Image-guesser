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

public class RegisterModel : PageModel
{
    private readonly IMediator _mediator;
    public RegisterModel(IMediator mediator) => _mediator = mediator;

    public List<User> Users { get; set; } = new();

    public string[] Errors { get; private set; } = System.Array.Empty<string>();

    [BindProperty]
    public User? user { get; set; }

    public async Task OnGetAsync()
        => Users = await _mediator.Send(new Core.Domain.User.Pipelines.Get.Request());

    public async Task<IActionResult> OnPostAsync()
    {
        if (user is null){
            return Page();
        }
        var result = await _mediator.Send(new Core.Domain.User.Pipelines.CheckIfUserExists.Request(user.Username));
        if (result.Success) // returns true if username is not taken
        {
            var id = Guid.NewGuid();
            var createResult = await _mediator.Send(new Core.Domain.User.Pipelines.Create.Request(user.Name, user.Username, user.Password, id));
            if (createResult.Success)
            {
                return RedirectToPage("./Index");
            }
            Errors = createResult.Errors;
            return Page();
        }
        Errors = result.Errors;
        return Page();

        
    }
}