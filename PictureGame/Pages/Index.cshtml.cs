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

	public User? user { get; set; }

	public string[] Errors { get; private set; } = System.Array.Empty<string>();

	[BindProperty]
	public string Username { get; set; }
	[BindProperty]
	public string Password { get; set; }

	public async Task OnGetAsync()
		=> Page();

	public async Task<IActionResult> OnPostAsync()
	{
		if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
		{
			Errors = new string[] { "Username and/or password is empty" };
			return Page();
		}
		user = await _mediator.Send(new Core.Domain.User.Pipelines.GetByUser.Request(Username, Password));
		if (user != null)
			{
				HttpContext.Session.SetString("UserId", user.Id.ToString());
				return RedirectToPage("./Menu");
			}
		Errors = new string[] { "Username and/or password is incorrect" };
		return Page();
			
	}
}
