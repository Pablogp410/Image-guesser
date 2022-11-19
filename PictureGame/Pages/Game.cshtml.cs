using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using PictureGame.Core.Domain.User;
using PictureGame.Core.Domain.Game;
using PictureGame.Core.Domain.Player;
using PictureGame.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;	
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using PictureGame.Core.Domain.User.Pipelines;

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

	public User? user { get; set; }

	public Player? player { get; set; }


	public string[] Errors { get; private set; } = System.Array.Empty<string>();
	public async Task OnGetAsync(){
		//Getting the session ID
		var UserId = HttpContext.Session.GetGuid("UserId");
		if(UserId == null){
			RedirectToPage("./Index");
			return;
		}
		//Getting the user using the UserId
		user = await _mediator.Send(new GetById.Request(UserId.Value));
		if(user == null){
			RedirectToPage("./Index");
			return;
		}
		//Users = await _mediator.Send(new Core.Domain.User.Pipelines.Get.Request());

		//Checking if there is any player with that user ID, if not create one
		player = await _mediator.Send(new Core.Domain.Player.Pipelines.GetPlayerById.Request(UserId.Value));
		if(player == null){
			user = await _mediator.Send(new Core.Domain.User.Pipelines.GetById.Request(UserId.Value));
			if(user == null){
				RedirectToPage("./Index");
				return;
			}
			player = await _mediator.Send(new Core.Domain.Player.Pipelines.CreatePlayer.Request(UserId.Value, user.Username));
		}
		//Checking if there are any existing games for the player
		game = await _mediator.Send(new Core.Domain.Game.Pipelines.GetGame.Request(player.Id));
		
		if (game == null){
			await _mediator.Send(new Core.Domain.Game.Pipelines.CreateGame.Request(player.Id));
			game = await _mediator.Send(new Core.Domain.Game.Pipelines.GetGame.Request(player.Id));
		}

		/*
		var UserId = HttpContext.Session.GetGuid("UserId");
		if(UserId != null){
			//Getting the user using the UserId
			user = await _mediator.Send(new GetById.Request(UserId.Value));
			if(user != null){
				//Checking if there is any player with that user ID, if not create one
				player = await _mediator.Send(new Core.Domain.Player.Pipelines.GetPlayerById.Request(UserId.Value));
				if(player == null){
					user = await _mediator.Send(new Core.Domain.User.Pipelines.GetById.Request(UserId.Value));
					if(user != null){
						player = await _mediator.Send(new Core.Domain.Player.Pipelines.CreatePlayer.Request(UserId.Value, user.Username));
						//Checking if there are any existing games for the player
						game = await _mediator.Send(new Core.Domain.Game.Pipelines.GetGame.Request(player.Id));
						
						if (game == null){
							await _mediator.Send(new Core.Domain.Game.Pipelines.CreateGame.Request(player.Id));
							game = await _mediator.Send(new Core.Domain.Game.Pipelines.GetGame.Request(player.Id));
						}
					}
				}
			}
		}*/
	}

	public async Task<IActionResult> OnPostAsync(string Guess)
	{
		//Getting the session ID
		var UserId = HttpContext.Session.GetGuid("UserId");
		if(UserId == null){
			return RedirectToPage("/Index");
		}
		if (!game.TheImage.Pieces.Any())
		{
			await _mediator.Send(new Core.Domain.Game.Pipelines.DiscardGame.Request(player.Id));
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
