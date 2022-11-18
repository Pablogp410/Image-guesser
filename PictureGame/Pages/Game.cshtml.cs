using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using PictureGame.Core.Domain.User;
using PictureGame.Core.Domain.Game;
using PictureGame.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;	
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace PictureGame.Pages;

public class GameModel : PageModel
{
	private readonly IMediator _mediator;
	public GameModel(IMediator mediator) => _mediator = mediator;

	Random rand = new Random();
	public List<User> Users { get; set; } = new();

	public Game game {get; set;} = new();


	public string Guess { get; set; }

	public int GuessTries { get; set; }

	public User user { get; set; }


	public string[] Errors { get; private set; } = System.Array.Empty<string>();
	public async Task OnGetAsync(){
		Users = await _mediator.Send(new Core.Domain.User.Pipelines.Get.Request());
		game = await _mediator.Send(new Core.Domain.Game.Pipelines.GetGame.Request());
		
		if (game == null){
			await _mediator.Send(new Core.Domain.Game.Pipelines.CreateGame.Request());
			game = await _mediator.Send(new Core.Domain.Game.Pipelines.GetGame.Request());
		}
	}

	public async Task<IActionResult> OnPostAsync(string Guess)
	{
		if (!game.TheImage.Pieces.Any())
		{
			await _mediator.Send(new Core.Domain.Game.Pipelines.DiscardGame.Request());
			game = null;
			return Page();

		}	
		if(Guess == game.TheImage.Name){
			game.Completed = true;
			foreach (var i in game.TheImage.Pieces)
			{
				game.CurrentImages.Add(i);
				game.TheImage.Pieces.Remove(i);
			}
			return Page();

		}	
		
		else{
			
			return Page();
		}
	}
}
