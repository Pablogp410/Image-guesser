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
        public static string[] PathToImages = Directory.GetDirectories(@"wwwroot\images\");
        public string[] ImageIds = File.ReadAllLines(@"wwwroot\image_mapping.txt");
        public string[] ImageLabels = File.ReadAllLines(@"wwwroot\label_mapping.txt");


        
        public Picture GetRandomImage()
        {              
            DirectoryInfo ChosenImage = new DirectoryInfo(PathToImages[rand.Next(0, PathToImages.Count())]);
             
            //string[] pieces = Directory.GetFiles(ChosenImage);
            List<Piece> ThePieces = new List<Piece>();

            List<string> pieces = new List<string>();

            foreach (FileInfo file in ChosenImage.GetFiles())
            {
                string TheName = @"/images/" + ChosenImage.Name + "/" + file.Name;
                pieces.Add(TheName);
            }

            foreach (var Piece in pieces)
            {
                //ThePieces.Add(@"\PictureGame\Infrastructure\images\" + ChosenImage.Name + @"\" + Piece.Name); //expand
                ThePieces.Add(new Piece(Piece));
            }

            string ImageFolderName = ChosenImage.Name.Substring(0, 23);
        
            foreach (var line in ImageIds)
                {
                    if (line.Contains(ImageFolderName))
                    {
                        var ImageId = line.Split(" ").Last();
                        foreach (var label in ImageLabels)
                        {   
                            if (label.Contains(ImageId))
                            {
                            var ImageNameArr = label.Split(" ");
                            string ImageName = "";
                            for (int i = 0; i < ImageNameArr.Length; i++)
                            {
                                if ( i != ImageNameArr.Length - 1)
                                {
                                   ImageName = ImageNameArr[i] + " ";
                                }
                                else
                                {
                                    ImageName = ImageNameArr[i];
                                }
                            }
                            return new Picture(ImageId, ImageName, ThePieces);  
                            }   
                        }
                    }                    
                }
            return null;

           
            
    }   }