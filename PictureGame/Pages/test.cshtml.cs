using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using PictureGame.Core.Domain.User;
using PictureGame.Core.Domain.Game;
using PictureGame.Core.Domain.Player;
using PictureGame.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;	
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using PictureGame.Core.Domain.User.Pipelines;

namespace PictureGame.Pages;

public class TestModel : PageModel
{
    private readonly IGetRandomImageService _RANDOM_IMAGE_SERVICE;
    public TestModel(IGetRandomImageService RAn) => _RANDOM_IMAGE_SERVICE = RAn;

    public Picture? picture { get; set; }

    public IActionResult OnGet(){
        picture =  _RANDOM_IMAGE_SERVICE.GetRandomImage();
    
        return Page();

    }
    
}

