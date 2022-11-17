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
    public Picture(string id, string name, List<string> pieces)
    {
        Name = name;
        Pieces = pieces;
        Id = id;
    }

    public string Name { get; set; } 
    public List<string> Pieces { get; set; } 
    public string Id { get; set; }
}
}