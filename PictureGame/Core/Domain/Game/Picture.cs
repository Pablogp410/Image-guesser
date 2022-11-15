using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace PictureGame.Core.Domain.Game{
public class Picture
{
    public Picture(string name, List<Image> pieces)
    {
        Name = name;
        Pieces = pieces;
    }

    public string Name { get; set; } 
    public List<Image> Pieces { get; set; }
}
}