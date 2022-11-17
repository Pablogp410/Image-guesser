using System.Collections.Generic;
using System.Threading.Tasks;
using PictureGame.Core.Domain.Player;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PictureGame.Pages;

public class LeaderBoardMode : PageModel
{
	private readonly IMediator _mediator;

	public LeaderBoardMode(IMediator mediator) => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

	public List<Player> Players { get; set; } = new();

	public async Task OnGetAsync()
		=> Players = await _mediator.Send(new PictureGame.Core.Domain.Player.Pipelines.GetPlayers.Request());
        //var sortedList = list.OrderBy(m => m.Category);

}