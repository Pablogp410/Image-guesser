using System.Drawing;
namespace   PictureGame.Domain.Game
{
     public class Picture
    {
         public   Picture (string  name, List<Image> pieces)
        {
               Name  =  name;
               Pieces  =  pieces;
        }
         public   string  Name {  get ;  set ; }
         public List<Image> Pieces {  get ;  set ; }
    }
}