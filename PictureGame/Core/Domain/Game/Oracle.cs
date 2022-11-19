using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using PictureGame.SharedKernel;
using PictureGame.Core.Domain.Game;
using Microsoft.EntityFrameworkCore;

namespace PictureGame.Core.Domain.Game
{

public interface IProposer
{
    int GetPiece(Picture picture);
}

[Owned]
public class Oracle : BaseEntity,  IProposer
{
    public Oracle() 
    {}
    public int GetPiece(Picture picture)
    {
        Random rand = new Random(); 
        int index = rand.Next(picture.Pieces.Count());
        picture.Pieces.RemoveAt(index);
        return index;
    }
    public int Id { get; protected set; }

}
}