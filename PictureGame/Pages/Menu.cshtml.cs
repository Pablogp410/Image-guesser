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

public class MenuModel : PageModel
{
	private readonly IMediator _mediator;
	public MenuModel (IMediator mediator) => _mediator = mediator;
	public string username { get; set; }
	public User user { get; set; }

	public IActionResult OnGet()
	{
		username = HttpContext.Session.GetString("Username");
		if (username == null)
		{
			return RedirectToPage("./Index");
		}
		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		var UserId = HttpContext.Session.GetGuid("UserId");
		if(UserId == null){
			return RedirectToPage("./Index");
		}
		user = await _mediator.Send(new Core.Domain.User.Pipelines.GetById.Request(UserId.Value));
		if (user == null)
		{
			return RedirectToPage("./Index");
		}
		HttpContext.Session.SetString("UserId", user.Id.ToString());
		return RedirectToPage("./Game");
	}
}
