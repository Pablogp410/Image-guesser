using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PictureGame.Domain.Game;

public interface IOracleService
{
    Task<int> CreateGame(Player player);
}

public class OracleService : IOracleService
{
    private readonly GameContext _db;
    public OracleService(GameContext Db)
    {
        _db = Db;
    }
    public async Task<int> ChooseRandomImage(Player player)
    {
        try
        {
            var Game = new Game()
            {
                Id = Guid.NewGuid(),
                player = player,
                scoreboard = new ScoreBoard()
            };
            _db.Games.Add(Game);
            await _db.SaveChangesAsync();
            return 1;
        }
        catch (Exception e)
        {   
            throw e;
        }
    }
}