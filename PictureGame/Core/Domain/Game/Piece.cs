using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace PictureGame.Core.Domain.Game{
public class Piece
{
    public Piece()
    {}
    public Piece(string name)
    {
        Name = name;
    }
    public String Name { get; set; } 
    public int Id { get; set; }
}
}