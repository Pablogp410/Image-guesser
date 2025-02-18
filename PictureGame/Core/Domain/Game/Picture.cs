using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace PictureGame.Core.Domain.Game{
public class Picture
{
    public Picture()
    {}
    public Picture(string pictureId, string name, List<Piece> pieces)
    {
        Name = name;
        Pieces = pieces;
        PictureId = pictureId;
    }

    public String Name { get; set; } 
    public List<Piece> Pieces { get; set; } 
    public String PictureId { get; set; }
    public int Id { get; set; }
}
}