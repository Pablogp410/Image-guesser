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

public class IndexModel : PageModel
{
	private readonly IMediator _mediator;
	public IndexModel(IMediator mediator) => _mediator = mediator;

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
		Users = await _mediator.Send(new Core.Domain.User.Pipelines.Get.Request());
		foreach (var u in Users)
		{
			if (u.Name == user.Name && u.Password == user.Password && u.Username == user.Username)
			{
				HttpContext.Session.SetString("user", user.Name);
				return RedirectToPage("./Menu");
			}
		}
		return Page();
			
	}
}
