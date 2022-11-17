using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;using PictureGame.SharedKernel;
using PictureGame.Core.Domain.Game;

namespace PictureGame.Core.Domain.Game;
    
    
    public interface IGetRandomImageService
    {
        Picture? GetRandomImage();
    }

    public class GetRandomImageService : IGetRandomImageService
    {
        static Random rand = new Random(); 
        static string[] PathToImages = Directory.GetFiles(@"\PictureGame\Infrastructure\images");
        public string[] ImageIds = File.ReadAllLines(@"\PictureGame\Infrastructure\image_mappping.txt");
        public string[] ImageLabels = File.ReadAllLines(@"\PictureGame\Infrastructure\label_mapping.txt");


        
        public Picture? GetRandomImage()
        {              
            DirectoryInfo ChosenImage = new DirectoryInfo(PathToImages[rand.Next(0, PathToImages.Length)]);
             
            
            List<Piece> ThePieces = new List<Piece>();
            foreach (var Piece in ChosenImage.GetFiles())
            {
                //ThePieces.Add(@"\PictureGame\Infrastructure\images\" + ChosenImage.Name + @"\" + Piece.Name); //expand
                ThePieces.Add(new Piece(Piece.Name));
            }

            string ImageFolderName = ChosenImage.Name.Substring(22);

            foreach (var line in ImageIds)
                {
                    if (line.Contains(ImageFolderName))
                    {
                        var ImageId = line.Split(" ").Last();

                        foreach (var label in ImageLabels)
                        {   
                            if (label.Contains(ImageId))
                            {
                            var ImageName = label.Split(" ").Last();
                            return new Picture(ImageId, ImageName, ThePieces);  
                            }   
                        }
                    }                    
                }
            return null;
           
            
    }   }