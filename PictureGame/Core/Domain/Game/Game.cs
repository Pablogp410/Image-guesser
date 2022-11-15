using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using PictureGame.SharedKernel;
using PictureGame.Core.Domain.Game;

namespace PictureGame.Core.Domain.Game{

public class Game : BaseEntity 
{
    public Game() {
    }
    
    static void GetRandomImage()
    {
       public string[] PathToImages = Directory.GetFiles(@"C:~PictureGame\PictureGame.Core\Domain\Game\images");
       Random random = new Random();
       int ChosenImage = random.Next(0, PathToImages.Length);

    }
}
    
}
