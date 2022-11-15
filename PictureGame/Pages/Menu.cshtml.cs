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

	public List<User> Users { get; set; } = new();

	public string[] Errors { get; private set; } = System.Array.Empty<string>();

	public async Task OnGetAsync()
		=> Users = await _mediator.Send(new Core.Domain.User.Pipelines.Get.Request());

	/*public async Task<IActionResult> OnPostAsync()
	{
		
	}*/
}
