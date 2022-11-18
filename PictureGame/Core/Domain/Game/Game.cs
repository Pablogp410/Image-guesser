using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;using PictureGame.SharedKernel;
using PictureGame.Core.Domain.Game;
using PictureGame.Core.Domain.Player;

namespace PictureGame.Core.Domain.Game
{
public class Game : BaseEntity 
{
    public Game() 
    {
        Oracle = new Oracle();
    }

    public int Id { get; protected set; }
    public List<Piece> CurrentImages { get; set; } = new List<Piece>();
    public Picture? TheImage { get; set; }
    public Image<Rgba32> ImageBase = new(375, 500);
    public Oracle Oracle { get; set; }

    public int UserId {get; set;}

    public int Score { get; set; }

    public bool Completed { get; set; }
    public int GuessTries { get; set; }

    //public Player player1 { get; set; }

    public void AddPiece()
    {
        int index = Oracle.GetPiece(TheImage);
		CurrentImages.Add(TheImage.Pieces[index]);
    }
    
}
}
